using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.DTOs;
using Sales.API.Services.Customers;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/customers")]
public class CustomersController(ICustomerRestService customerRestService) : MainController
{
    private readonly ICustomerRestService _customerRestService = customerRestService;

    [HttpGet("address")]
    public async Task<IResult> GetAddressAsync()
        => CustomResponse(await _customerRestService.GetAddressAsync());

    [HttpPost]
    public async Task<IResult> AddAddressAsync(AddressDTO address)
        => CustomResponse(await _customerRestService.AddAddressAsync(address));
}
