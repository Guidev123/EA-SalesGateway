using MediatR;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Carts.ApplyVoucher;

public record ApplyVoucherCommand(string Code) : IRequest<Response>;
