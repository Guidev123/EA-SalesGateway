using Sales.API.DTOs;
using Sales.API.Responses;

namespace Sales.API.Services.Cart;

public interface ICartRestService
{
    Task<Response<AddItemToCartResponseDTO>> AddItemToCartAsync(CartItensDTO cartItens);
    Task<Response<CartDTO>> GetByCustomerIdAsync();
    Task<Response> ApplyVoucherToCartAsync(string voucherCode);
    Task<Response> RemoveItemFromCartAsync(Guid productId);
    Task<Response> UpdateCartItemAsync(Guid productId, int quantity);
}
