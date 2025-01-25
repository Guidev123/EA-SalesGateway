using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.API.DTOs;
using Sales.API.Services.Cart;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/cart")]
public class CartController(ICartRestService cartRest) : MainController
{
    private readonly ICartRestService _cartRest = cartRest;

    [HttpPost]
    public async Task<IResult> AddItemToCartAsync(CartItensDTO item) =>
        CustomResponse(await _cartRest.AddItemToCart(item));
}
