using MediatR;
using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Services.Carts;
using Sales.API.Services.Catalogs;
using Sales.API.Services.Customers;
using Sales.API.Services.Orders;

namespace Sales.API.Application.UseCases.Orders.Create;

public sealed class CreateOrderHandler(IOrderRestService orderService,
                    ICatalogRestService catalogService,
                    ICartRestService cartService,
                    ICustomerRestService customerService)
                  : IRequestHandler<CreateOrderCommand, Response<CreateOrderResponse>>
{
    private readonly IOrderRestService _orderService = orderService;
    private readonly ICatalogRestService _catalogService = catalogService;
    private readonly ICartRestService _cartService = cartService;
    private readonly ICustomerRestService _customerService = customerService;
    public async Task<Response<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = await ValidateAddress();
        var orderValidator = await ValidateOrderDetails(request, address);
        if (!orderValidator.IsSuccess)
            return new(null, orderValidator.StatusCode, orderValidator.Message, orderValidator.Errors);

        var result = await _orderService.CreateAsync(new(request.TotalPrice, request.OrderItems, request.VoucherCode,
                                                     request.VoucherIsUsed, request.Discount, address.Data!));

        return result.IsSuccess
            ? result
            : new(null, 400, result.Message, result.Errors);
    }

    private async Task<Response> ValidateOrderDetails(CreateOrderCommand request, Response<AddressDTO> address)
    {
        var cart = await ValidateCart();
        var products = await ValidateProducts(cart.Data!);
        if (!cart.IsSuccess || !products.IsSuccess || !address.IsSuccess)
            return new(400, "Invalid Operation", GetAllErrors(cart, products, address));

        var validateOrder = await ValidateOrder(cart.Data!, products.Data!);
        if (!validateOrder.IsSuccess)
            return new(400, validateOrder.Message, validateOrder.Errors);

        if (cart.Data!.TotalPrice != request.TotalPrice)
            return new(400, "The cart price is different from the order price");

        return new(200);
    }

    private async Task<Response> ValidateOrder(CartDTO cart, IEnumerable<OrderItemDTO> products)
    {
        var availabilityResponse = ValidateProductAvailability(cart, products);
        if (!availabilityResponse.IsSuccess) return availabilityResponse;

        return await ValidateProductPrices(cart, products);
    }

    private static Response ValidateProductAvailability(CartDTO cart, IEnumerable<OrderItemDTO> products)
    {
        if (cart.CartItems.Count() != products.Count())
        {
            var validItems = cart.CartItems.Select(c => c.ProductId).Except(products.Select(x => x.ProductId)).ToList();
            foreach (var itemId in validItems)
            {
                var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == itemId);
                if (cartItem is null) return new(400, "Products not found");

                return new(400, $"Item {cartItem.Name} is not available any more");
            }

            return new(400, "The order item is not available any more");
        }

        return new(200);
    }

    private async Task<Response> ValidateProductPrices(CartDTO cart, IEnumerable<OrderItemDTO> products)
    {
        foreach (var item in cart.CartItems)
        {
            var errorList = new List<string>();

            var catalogProduct = products.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (catalogProduct is null)
                return new(400, "The product no longer exists");

            if (catalogProduct.Price != item.Price)
            {
                var errorMessage = $"The product {item.Name} has changed in price (from: {item.Price} to: {catalogProduct.Price})";
                errorList.Add(errorMessage);

                var responseRemove = await _cartService.RemoveItemAsync(item.ProductId);

                if (!responseRemove.IsSuccess)
                    errorList.Add($"It was not possible to automatically remove the product {item.Name}, please remove and add it again if you still want this item");

                var responseAddItem = await _cartService.AddItemAsync(new(item.ProductId, item.Name, catalogProduct.Price, item.ImageUrl, item.Quantity));
                if (!responseAddItem.IsSuccess)
                    errorList.Add("It was not possible to automatically update the cart, please add the item again if you still want it");

                return new(400, "Invalid Operation", errorList.ToArray());
            }
        }
        return new(200);
    }


    private async Task<Response<CartDTO>> ValidateCart()
    {
        var cartResponse = await _cartService.GetByCustomerIdAsync();
        if (!cartResponse.IsSuccess || cartResponse.Data is null)
            return new(null, 400, cartResponse.Message);

        return new(cartResponse.Data, 200);
    }

    private async Task<Response<AddressDTO>> ValidateAddress()
    {
        var addressResponse = await _customerService.GetAddressAsync();
        if (!addressResponse.IsSuccess || addressResponse.Data is null)
            return new(null, 400, addressResponse.Message);

        return new(addressResponse.Data, 200);
    }

    private async Task<Response<IEnumerable<OrderItemDTO>>> ValidateProducts(CartDTO cart)
    {
        var products = await _catalogService.GetByIdsAsync(cart.CartItems.Select(x => x.ProductId));
        if (!products.IsSuccess || products.Data is null
            || !products.Data.Any()) return new(null, 400, products.Message);

        return new(products.Data, 200);
    }

    private static string[] GetAllErrors(Response<CartDTO> cart, Response<IEnumerable<OrderItemDTO>> products, Response<AddressDTO> address)
    {
        var errorList = new List<string>();

        if (cart.Errors is not null)
            errorList.AddRange(cart.Errors);

        if (products.Errors is not null)
            errorList.AddRange(products.Errors);

        if (address.Errors is not null)
            errorList.AddRange(address.Errors);

        return errorList.ToArray();
    }
}
