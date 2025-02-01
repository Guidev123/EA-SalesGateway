using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Carts.AddItem;

namespace Sales.API.Services.Carts;

public interface ICartRestService
{
    Task<Response<AddItemToCartResponse>> AddItemAsync(AddItemToCartCommand cartItens);
    Task<Response<CartDTO>> GetByCustomerIdAsync();
    Task<Response> ApplyVoucherAsync(string voucherCode);
    Task<Response> RemoveItemAsync(Guid productId);
    Task<Response> UpdateItemAsync(Guid productId, int quantity);
}
