using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Customers;

public sealed class CustomerRestService(HttpClient client) : Service, ICustomerRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<AddressDTO>> GetAddressAsync()
    {
        throw new NotImplementedException();
    }
}
