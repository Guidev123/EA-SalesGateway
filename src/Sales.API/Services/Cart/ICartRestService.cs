using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Cart.AddItem;
using Sales.API.DTOs;

namespace Sales.API.Services.Cart;

public interface ICartRestService
{
    Task<Response<AddItemToCartResponse>> AddItemToCartAsync(AddItemToCartRequest cartItens);
    Task<Response<CartDTO>> GetByCustomerIdAsync();
    Task<Response> ApplyVoucherToCartAsync(string voucherCode);
    Task<Response> RemoveItemFromCartAsync(Guid productId);
    Task<Response> UpdateCartItemAsync(Guid productId, int quantity);
}
