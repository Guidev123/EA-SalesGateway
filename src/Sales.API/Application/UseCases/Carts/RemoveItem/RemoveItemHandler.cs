using MediatR;
using Sales.API.Application.Responses;
using Sales.API.Services.Cart;

namespace Sales.API.Application.UseCases.Carts.RemoveItem;

public sealed class RemoveItemHandler(ICartRestService cartService)
                  : IRequestHandler<RemoveItemCommand, Response>
{
    private readonly ICartRestService _cartService = cartService;

    public async Task<Response> Handle(RemoveItemCommand command, CancellationToken cancellationToken)
        => await _cartService.RemoveItemAsync(command.ProductId);
}
