using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Payments.Create;

namespace Sales.API.Services.Payments;

public interface IPaymentRestService
{
    Task<Response<CreatePaymentResponse>> CreatePaymentAsync(CreatePaymentCommand command);
    Task<Response<IEnumerable<StripeTransactionDTO>>> GetStripeTransactionAsync(string orderCode);
}
