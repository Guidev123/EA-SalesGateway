using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.UseCases.Orders.Create;
using Sales.API.Configurations;
using Sales.API.Services.Orders;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/order")]
public class OrdersController(IMediator mediator, IOrderRestService orderRestService) : MainController
{
    private readonly IMediator _mediator = mediator;
    private readonly IOrderRestService _orderRestService = orderRestService;

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateOrderCommand command)
        => CustomResponse(await _mediator.Send(command));

    [HttpGet]
    public async Task<IResult> GetAllAsync(int pageNumber = ApiConfiguration.DEFAULT_PAGE,
                                           int pageSize = ApiConfiguration.DEFAULT_PAGE_SIZE)
        => CustomResponse(await _orderRestService.GetAllAsync(pageNumber, pageSize));

    [HttpGet("{code}")]
    public async Task<IResult> GetByIdAsync(string code)
        => CustomResponse(await _orderRestService.GetByCodeAsync(code));
}
