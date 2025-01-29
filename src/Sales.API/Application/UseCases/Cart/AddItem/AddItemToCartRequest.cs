using MediatR;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Cart.AddItem;

public record AddItemToCartRequest(Guid ProductId, string Name, decimal Price, string Image, int Quantity)
    : IRequest<Response<AddItemToCartResponse>>;
