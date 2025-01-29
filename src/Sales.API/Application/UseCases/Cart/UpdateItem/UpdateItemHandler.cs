using MediatR;
using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Services.Cart;
using Sales.API.Services.Catalog;

namespace Sales.API.Application.UseCases.Cart.UpdateItem;

public sealed class UpdateItemHandler(ICartRestService cartService,
                    ICatalogRestService catalogService)
                  : IRequestHandler<UpdateItemCommand, Response>
{
    private readonly ICartRestService _cartService = cartService;
    private readonly ICatalogRestService _catalogService = catalogService;

    public async Task<Response> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
    {
        var validate = await ValidateItemQuantity(command.ProductId, command.Quantity);
        if(!validate.IsSuccess)
            return new(validate.StatusCode, validate.Message);

        return await _cartService.UpdateCartItemAsync(command.ProductId, command.Quantity);
    }

    private async Task<Response<ProductDTO>> ValidateItemQuantity(Guid productId, int quantity)
    {
        var product = await _catalogService.GetProductByIdAsync(productId);
        if(product.Data is null || !product.IsSuccess)
            return new(null, 404, "Product not found");

        var cart = await _cartService.GetByCustomerIdAsync();
        if (cart.Data is null || !cart.IsSuccess)
            return new(null, 404, "Cart not found");

        var itemCart = cart.Data.Items.FirstOrDefault(p => p.ProductId == productId);

        if(itemCart is not null && itemCart.Quantity + quantity > product.Data.QuantityInStock)
            return new(null, 400, $"The product {product.Data.Name} has {product.Data.QuantityInStock} unities in stock, you picked up {quantity}");

        if(quantity > product.Data.QuantityInStock)
            return new(null, 400, $"The product {product.Data.Name} has {product.Data.QuantityInStock} unities in stock, you picked up {quantity}");

        return new(null, 204);
    }
}
