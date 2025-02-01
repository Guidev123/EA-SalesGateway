using MediatR;
using Sales.API.Application.Responses;
using Sales.API.Services.Orders;
using Sales.API.Services.Payments;

namespace Sales.API.Application.UseCases.Payments.Create;

public sealed class CreatePaymentHandler(IPaymentRestService paymentRestService, IOrderRestService orderRestService)
                  : IRequestHandler<CreatePaymentCommand, Response<CreatePaymentResponse>>
{
    private readonly IPaymentRestService _paymentRestService = paymentRestService;
    private readonly IOrderRestService _orderRestService = orderRestService;
    public async Task<Response<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRestService.GetByCodeAsync(request.OrderCode);
        if (!order.IsSuccess || order.Data is null)
            return new(null, 400, order.Message, order.Errors);

        if (order.Data.TotalPrice != request.Total)
            return new(null, 400, "The order price is diferent");

        var result = await _paymentRestService.CreatePaymentAsync(request);

        return result.IsSuccess ? result : new(null, 400, result.Message, result.Errors);
    }
}
