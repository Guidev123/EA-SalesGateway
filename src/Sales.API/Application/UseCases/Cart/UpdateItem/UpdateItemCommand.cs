using MediatR;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Cart.UpdateItem;

public record UpdateItemCommand(int Quantity, Guid ProductId) : IRequest<Response>;
