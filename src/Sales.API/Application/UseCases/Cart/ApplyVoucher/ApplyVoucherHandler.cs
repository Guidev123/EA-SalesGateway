using MediatR;
using Sales.API.Application.Responses;
using Sales.API.Services.Cart;
using Sales.API.Services.Order;

namespace Sales.API.Application.UseCases.Cart.ApplyVoucher;

public sealed class ApplyVoucherHandler(IOrderRestService orderService,
                                        ICartRestService cartService)
                  : IRequestHandler<ApplyVoucherCommand, Response>
{
    private readonly IOrderRestService _orderService = orderService;
    private readonly ICartRestService _cartService = cartService;
    public async Task<Response> Handle(ApplyVoucherCommand command, CancellationToken cancellationToken)
    {
        var voucher = await _orderService.GetVoucherByCode(command.Code);
        if (voucher.IsSuccess || voucher.Data is null)
            return new(404, voucher.Message);

        var result = await _cartService.ApplyVoucherAsync(voucher.Data.Code);

        return result.IsSuccess
            ? new(204, "Success!")
            : new(400, result.Message);
    }
}
