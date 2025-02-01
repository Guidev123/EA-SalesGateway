using MediatR;
using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Orders.Create;

public record CreateOrderCommand(decimal TotalPrice, List<OrderItemDTO> OrderItems,
                                  string VoucherCode, bool VoucherIsUsed,
                                  decimal Discount)
                                : IRequest<Response<CreateOrderResponse>>;
