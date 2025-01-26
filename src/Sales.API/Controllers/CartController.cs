using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.DTOs;
using Sales.API.Services.Cart;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/cart")]
public class CartController(ICartRestService cartRest) : MainController
{
    private readonly ICartRestService _cartRest = cartRest;

    [HttpPost]
    public async Task<IResult> AddItemToCartAsync(CartItensDTO item) =>
        CustomResponse(await _cartRest.AddItemToCartAsync(item));

    [HttpGet]
    public async Task<IResult> GetByCustomerIdAsync() =>
        CustomResponse(await _cartRest.GetByCustomerIdAsync());

    [HttpPut("/{productId:guid}/{quantity:int}")]
    public async Task<IResult> UpdateCartItemAsync(Guid productId, int quantity) =>
        CustomResponse(await _cartRest.UpdateCartItemAsync(productId, quantity));

    [HttpDelete("/{productId:guid}")]
    public async Task<IResult> RemoveItemFromCartAsync(Guid productId) =>
        CustomResponse(await _cartRest.RemoveItemFromCartAsync(productId));
}
