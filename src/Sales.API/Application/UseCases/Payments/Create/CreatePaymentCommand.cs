using MediatR;
using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Payments.Create;

public record CreatePaymentCommand(string OrderCode,
                                   decimal Total, int Gateway,
                                   int PaymentType, TransactionDTO Transaction) : IRequest<Response<CreatePaymentResponse>>;
