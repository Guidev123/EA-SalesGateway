using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Customers;

public interface ICustomerRestService
{
    Task<Response<AddressDTO>> GetAddressAsync();
}
