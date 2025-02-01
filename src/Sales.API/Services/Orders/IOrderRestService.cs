using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Orders.Create;

namespace Sales.API.Services.Orders;

public interface IOrderRestService
{
    Task<Response<VoucherDTO>> GetVoucherByCodeAsync(string code);
    Task<Response<CreateOrderResponse>> CreateAsync(CreateOrderDTO order);
    Task<PagedResponse<IEnumerable<OrderDTO>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Response<OrderDTO>> GetByCodeAsync(string code);
}
