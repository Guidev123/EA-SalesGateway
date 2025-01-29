using MediatR;
using Sales.API.Application.Responses;
using Sales.API.DTOs;
using Sales.API.Services.Cart;
using Sales.API.Services.Catalog;

namespace Sales.API.Application.UseCases.Cart.AddItem;

public sealed class AddItemToCartHandler(ICartRestService cartService, ICatalogRestService catalogService)
                  : IRequestHandler<AddItemToCartRequest, Response<AddItemToCartResponse>>
{
    private readonly ICartRestService _cartService = cartService;
    private readonly ICatalogRestService _catalogService = catalogService;
    public async Task<Response<AddItemToCartResponse>> Handle(AddItemToCartRequest request, CancellationToken cancellationToken)
    {
        var product = await _catalogService.GetProductByIdAsync(request.ProductId);
        if (!product.IsSuccess || product.Data is null)
            return new(null, 404, "Product not found");

        var cart = await _cartService.GetByCustomerIdAsync();
        var cartValidation = ValidateItemCart(cart.Data, product.Data, request.ProductId, request.Quantity);

        if (!cartValidation.IsSuccess)
            return new(null, 400, cartValidation.Message);

        var result = await _cartService.AddItemToCartAsync(request);
        return result is null ? new(null, 400) : result;
    }

    private static Response<ProductDTO> ValidateItemCart(CartDTO? cart, ProductDTO product, Guid productId, int quantity) 
        => cart is null
            ? HandleNewCartItem(product, quantity)
            : HandleCartItem(cart, product, productId, quantity);

    private static Response<ProductDTO> HandleCartItem(CartDTO cart, ProductDTO productDTO, Guid productId, int quantity)
    {
        if (productDTO is null || cart is null) return new(null, 400, "Product is null");
        if (quantity < 1) return new(null, 400, $"The quantity of {productDTO.Name} should be greater than 0");

        var itemCart = cart.Items.FirstOrDefault(p => p.ProductId == productId);

        if (itemCart is not null && itemCart.Quantity + quantity > productDTO.QuantityInStock)
            return new(null, 400, $"The product {productDTO.Name} has {productDTO.QuantityInStock} unities in stock, you picked up {quantity}");

        if (quantity > productDTO.QuantityInStock)
            return new(null, 400, $"The product {productDTO.Name} has {productDTO.QuantityInStock} unities in stock, you picked up {quantity}");

        return new(null, 200);
    }

    private static Response<ProductDTO> HandleNewCartItem(ProductDTO productDTO, int quantity)
    {
        if (productDTO is null) return new(null, 400, "Product is null");
        if (quantity < 1) return new(null, 400, $"The quantity of {productDTO.Name} should be greater than 0");

        if (quantity > productDTO.QuantityInStock)
            return new(null, 400, $"The product {productDTO.Name} has {productDTO.QuantityInStock} unities in stock, you picked up {quantity}");

        return new(null, 200);
    }
}
