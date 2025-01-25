using Sales.API.DTOs;
using Sales.API.Responses;

namespace Sales.API.Services.Cart;

public sealed class CartRestService : Service, ICartRestService
{
    private readonly HttpClient _client;

    public CartRestService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<CreateCartResponseDTO>> AddItemToCart(CartItensDTO cartItens)
    {
        var response = await _client.PostAsync("/cart/items", GetContent(cartItens));

        if (!OpertationIsValid(response))
        {
            var result = await DeserializeObjectResponse<Response<CreateCartResponseDTO>>(response);
            return result is null
                ? new(null, 400)
                : result;
        }

        return new(null, 400);
    }
}

