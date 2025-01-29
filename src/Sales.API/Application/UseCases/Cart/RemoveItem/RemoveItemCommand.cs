using MediatR;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Cart.RemoveItem;

public record RemoveItemCommand(Guid ProductId) : IRequest<Response>;
