using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Orders.Create;

namespace Sales.API.Services.Orders;

public sealed class OrderRestService(HttpClient client)
                  : Service, IOrderRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<CreateOrderResponse>> CreateAsync(CreateOrderDTO order)
    {
        var response = await _client.PostAsync("/api/v1/orders", GetContent(order)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<CreateOrderResponse>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<PagedResponse<IEnumerable<OrderDTO>>> GetAllAsync(int pageNumber, int pageSize)
    {
        var response = await _client.GetAsync($"/api/v1/orders?pageNumber={pageNumber}&pageSize={pageSize}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<PagedResponse<IEnumerable<OrderDTO>>>(response);
        return result is not null
            ? new(result.TotalCount, result.Data, result.CurrentPage, result.PageSize, 200, "Valid Operation")
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response<OrderDTO>> GetByCodeAsync(string code)
    {
        var response = await _client.GetAsync($"/api/v1/orders/{code}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<OrderDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
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
