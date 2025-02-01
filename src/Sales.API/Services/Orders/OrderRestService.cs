using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Orders.Create;

namespace Sales.API.Services.Orders;

public sealed class OrderRestService(HttpClient client)
                  : Service, IOrderRestService
{
    private readonly HttpClient _client = client;

    public Task<Response<CreateOrderResponse>> CreateAsync(CreateOrderCommand order)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResponse<OrderDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Response<OrderDTO>> GetByCodeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<VoucherDTO>> GetVoucherByCodeAsync(string code)
    {
        var response = await _client.GetAsync($"/{code}").ConfigureAwait(false);
        var result = await DeserializeObjectResponse<Response<VoucherDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
