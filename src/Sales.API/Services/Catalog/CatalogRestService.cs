using Sales.API.Application.Responses;
using Sales.API.DTOs;

namespace Sales.API.Services.Catalog;

public sealed class CatalogRestService(HttpClient client)
                  : Service, ICatalogRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<ProductDTO>> GetProductByIdAsync(Guid id)
    {
        var response = await _client.GetAsync($"/{id}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<ProductDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
