using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Customers;

public sealed class CustomerRestService(HttpClient client) : Service, ICustomerRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response> AddAddressAsync(AddressDTO address)
    {
        var response = await _client.PostAsync("/api/v1/customers", GetContent(address)).ConfigureAwait(false);
        if ((int)response.StatusCode == 201)
            return new(201, "Operation is valid");

        var result = await DeserializeObjectResponse<Response>(response);
        return result is not null
            ? new((int)response.StatusCode, result.Message, result.Errors)
            : new(400, "Something failed during the request.");
    }

    public async Task<Response<AddressDTO>> GetAddressAsync()
    {
        var response = await _client.GetAsync("/api/v1/customers/address");

        var result = await DeserializeObjectResponse<Response<AddressDTO>>(response);
        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
