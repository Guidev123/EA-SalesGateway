using Sales.API.DTOs;
using Sales.API.Responses;

namespace Sales.API.Services.Cart;

public interface ICartRestService
{
    Task<Response<CreateCartResponseDTO>> AddItemToCart(CartItensDTO cartItens);
}
