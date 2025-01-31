using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Orders.Create;

namespace Sales.API.Services.Order;

public interface IOrderRestService
{
    Task<Response<VoucherDTO>> GetVoucherByCodeAsync(string code);
    Task<Response<CreateOrderResponse>> CreateAsync(CreateOrderCommand order);
    Task<PagedResponse<OrderDTO>> GetAllAsync();
    Task<Response<OrderDTO>> GetByCodeAsync();
}
