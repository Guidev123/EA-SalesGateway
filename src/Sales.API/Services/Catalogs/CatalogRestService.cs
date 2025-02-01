using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Catalogs;

public sealed class CatalogRestService(HttpClient client)
                  : Service, ICatalogRestService
{
    private readonly HttpClient _client = client;

    public async Task<PagedResponse<IEnumerable<ProductDTO>>> GetAllAsync()
    {
        var response = await _client.GetAsync("/api/v1/catalog").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<PagedResponse<IEnumerable<ProductDTO>>>(response);

        return result is not null
            ? new(result.TotalCount, result.Data, result.CurrentPage, result.PageSize, 200, "Valid Operation")
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response<ProductDTO>> GetByIdAsync(Guid id)
    {
        var response = await _client.GetAsync($"/api/v1/catalog/{id}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<ProductDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response<IEnumerable<OrderItemDTO>>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var idsRequest = string.Join(",", ids);
        var response = await _client.GetAsync($"/api/v1/catalog/list/{idsRequest}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<IEnumerable<OrderItemDTO>>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
