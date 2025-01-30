using MediatR;
using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Services.Cart;
using Sales.API.Services.Catalog;

namespace Sales.API.Application.UseCases.Carts.AddItem;

public sealed class AddItemToCartHandler(ICartRestService cartService, ICatalogRestService catalogService)
    : IRequestHandler<AddItemToCartCommand, Response<AddItemToCartResponse>>
{
    private readonly ICartRestService _cartService = cartService;
    private readonly ICatalogRestService _catalogService = catalogService;

    public async Task<Response<AddItemToCartResponse>> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
    {
        var product = await _catalogService.GetByIdAsync(command.ProductId);
        if (!product.IsSuccess || product.Data is null)
            return new(null, 404, "Product not found");

        var cart = await _cartService.GetByCustomerIdAsync();
        var validationResult = cart.IsSuccess && cart.Data is not null
            ? ValidateCartItem(cart.Data, product.Data, command.ProductId, command.Quantity)
            : ValidateNewCartItem(product.Data, command.Quantity);

        if (!validationResult.IsSuccess)
            return new(null, 400, validationResult.Message);

        var result = await _cartService.AddItemAsync(command);
        return result ?? new(null, 400, "Invalid operation");
    }

    private static Response<ProductDTO> ValidateCartItem(CartDTO cart, ProductDTO productDTO, Guid productId, int quantity)
    {
        if (cart is null) return new(null, 400, "Cart is null");

        var existingItem = cart.CartItems.FirstOrDefault(p => p.ProductId == productId);
        return ValidateNewCartItem(productDTO, existingItem?.Quantity + quantity ?? quantity);
    }

    private static Response<ProductDTO> ValidateNewCartItem(ProductDTO productDTO, int quantity)
    {
        if (productDTO is null) return new(null, 400, "Product is null");
        if (quantity < 1) return new(null, 400, $"The quantity of {productDTO.Name} should be greater than 0");
        if (quantity > productDTO.QuantityInStock)
            return new(null, 400, $"The product {productDTO.Name} has {productDTO.QuantityInStock} units in stock, you picked {quantity}");

        return new(null, 200, "Success!");
    }
}
