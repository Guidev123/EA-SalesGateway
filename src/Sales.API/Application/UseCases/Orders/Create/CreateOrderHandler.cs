using MediatR;
using Sales.API.Application.Responses;
using Sales.API.Services.Order;

namespace Sales.API.Application.UseCases.Orders.Create;

public sealed class CreateOrderHandler(IOrderRestService orderService)
                  : IRequestHandler<CreateOrderRequest, Response<CreateOrderResponse>>
{
    private readonly IOrderRestService _orderService = orderService;
    public async Task<Response<CreateOrderResponse>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
