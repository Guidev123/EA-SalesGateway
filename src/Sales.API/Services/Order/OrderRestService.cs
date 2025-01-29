using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Order;

public sealed class OrderRestService(HttpClient client)
                  : Service, IOrderRestService
{
    private readonly HttpClient _client = client;
    public async Task<Response<VoucherDTO>> GetVoucherByCode(string code)
    {
        var response = await _client.GetAsync($"/{code}").ConfigureAwait(false);
        var result = await DeserializeObjectResponse<Response<VoucherDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
