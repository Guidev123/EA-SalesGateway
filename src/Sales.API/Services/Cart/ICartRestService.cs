using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Cart.AddItem;

namespace Sales.API.Services.Cart;

public interface ICartRestService
{
    Task<Response<AddItemToCartResponse>> AddItemToCartAsync(AddItemToCartCommand cartItens);
    Task<Response<CartDTO>> GetByCustomerIdAsync();
    Task<Response> ApplyVoucherToCartAsync(string voucherCode);
    Task<Response> RemoveItemFromCartAsync(Guid productId);
    Task<Response> UpdateCartItemAsync(Guid productId, int quantity);
}
