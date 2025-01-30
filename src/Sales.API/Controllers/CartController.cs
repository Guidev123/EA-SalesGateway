using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Cart.AddItem;
using Sales.API.Application.UseCases.Cart.ApplyVoucher;
using Sales.API.Application.UseCases.Cart.RemoveItem;
using Sales.API.Application.UseCases.Cart.UpdateItem;
using Sales.API.Services.Cart;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/cart")]
public class CartController(IMediator mediator, ICartRestService cartService) : MainController
{
    private readonly IMediator _mediator = mediator;
    private readonly ICartRestService _cartService = cartService;

    [HttpPost]
    public async Task<IResult> AddItemToCartAsync(AddItemToCartCommand command) =>
        CustomResponse(await _mediator.Send(command));

    [HttpPost("apply-voucher/{code}")]
    public async Task<IResult> ApplyVoucherToCartAsync(string code) =>
        CustomResponse(await _mediator.Send(new ApplyVoucherCommand(code)));

    [HttpGet]
    public async Task<IResult> GetByCustomerIdAsync() =>
        CustomResponse(await _cartService.GetByCustomerIdAsync());


    [HttpPut("{productId:guid}/{quantity:int}")]
    public async Task<IResult> UpdateCartItemAsync(Guid productId, int quantity) =>
        CustomResponse(await _mediator.Send(new UpdateItemCommand(quantity, productId)));

    [HttpDelete("{productId:guid}")]
    public async Task<IResult> RemoveItemFromCartAsync(Guid productId) =>
        CustomResponse(await _mediator.Send(new RemoveItemCommand(productId)));

    [HttpGet("quantity")]
    public async Task<IResult> GetCartQuantityItems()
    {
        var cart = await _cartService.GetByCustomerIdAsync();
        if (!cart.IsSuccess
            || cart.Data is null
            || cart.Data.CartItems is null)
            return CustomResponse(new Response(404, "Cart not found"));

        int quantity = 0;
        cart.Data.CartItems.ForEach(x =>
        {
            quantity += x.Quantity;
        });

        var result = new Response<int>(quantity, 200, "Success!");
        return CustomResponse(result);
    }
}
