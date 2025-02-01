using MediatR;
using Sales.API.Application.Responses;
using Sales.API.Services.Carts;
using Sales.API.Services.Orders;

namespace Sales.API.Application.UseCases.Carts.ApplyVoucher;

public sealed class ApplyVoucherHandler(IOrderRestService orderService,
                                        ICartRestService cartService)
                  : IRequestHandler<ApplyVoucherCommand, Response>
{
    private readonly IOrderRestService _orderService = orderService;
    private readonly ICartRestService _cartService = cartService;
    public async Task<Response> Handle(ApplyVoucherCommand command, CancellationToken cancellationToken)
    {
        var voucher = await _orderService.GetVoucherByCodeAsync(command.Code);
        if (voucher.IsSuccess || voucher.Data is null)
            return new(404, voucher.Message);

        var result = await _cartService.ApplyVoucherAsync(voucher.Data.Code);

        return result.IsSuccess
            ? new(204, "Success!")
            : new(400, result.Message);
    }
}
