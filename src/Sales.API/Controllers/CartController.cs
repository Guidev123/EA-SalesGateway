using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.UseCases.Cart.AddItem;
using Sales.API.Services.Cart;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/cart")]
public class CartController(IMediator mediator) : MainController
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IResult> AddItemToCartAsync(AddItemToCartRequest command) =>
        CustomResponse(await _mediator.Send(command));

    [HttpPost("/apply-voucher/{code}")]
    public async Task<IResult> ApplyVoucherToCartAsync(string code) =>
        CustomResponse(await _cartRest.ApplyVoucherToCartAsync(code));

    [HttpGet]
    public async Task<IResult> GetByCustomerIdAsync() =>
        CustomResponse(await _cartRest.GetByCustomerIdAsync());

    [HttpGet("/quantity")]
    public async Task<IResult> GetCartQuantityItems() =>
        CustomResponse(await _cartRest.GetByCustomerIdAsync());

    [HttpPut("/{productId:guid}/{quantity:int}")]
    public async Task<IResult> UpdateCartItemAsync(Guid productId, int quantity) =>
        CustomResponse(await _cartRest.UpdateCartItemAsync(productId, quantity));

    [HttpDelete("/{productId:guid}")]
    public async Task<IResult> RemoveItemFromCartAsync(Guid productId) =>
        CustomResponse(await _cartRest.RemoveItemFromCartAsync(productId));
}
