using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.UseCases.Payments.Create;
using Sales.API.Services.Payments;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/payment")]
public class PaymentsController(IMediator mediator, IPaymentRestService paymentRestService) : MainController
{
    private readonly IMediator _mediator = mediator;
    private readonly IPaymentRestService _paymentRestService = paymentRestService;

    [HttpPost]
    public async Task<IResult> CreateAsync(CreatePaymentCommand command)
        => CustomResponse(await _mediator.Send(command));

    [HttpGet("{code}")]
    public async Task<IResult> GetByCodeAsync(string code)
        => CustomResponse(await _paymentRestService.GetStripeTransactionAsync(code));

}
