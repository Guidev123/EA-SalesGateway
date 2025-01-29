using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Order;

public interface IOrderRestService
{
    Task<Response<VoucherDTO>> GetVoucherByCode(string code);
}
