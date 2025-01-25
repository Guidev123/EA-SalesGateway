using Sales.API.DTOs;
using Sales.API.Responses;

namespace Sales.API.Services.Cart;

public sealed class CartRestService(HttpClient client)
                  : Service, ICartRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<CreateCartResponseDTO>> AddItemToCart(CartItensDTO cartItens)
    {
        var response = await _client.PostAsync(string.Empty, GetContent(cartItens)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<CreateCartResponseDTO>>(response);

        return result is not null ? result : new(null, 400, "Something failed during the request.");
    }
}

