﻿using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Cart.AddItem;

namespace Sales.API.Services.Cart;

public interface ICartRestService
{
    Task<Response<AddItemToCartResponse>> AddItemAsync(AddItemToCartCommand cartItens);
    Task<Response<CartDTO>> GetByCustomerIdAsync();
    Task<Response> ApplyVoucherAsync(string voucherCode);
    Task<Response> RemoveItemAsync(Guid productId);
    Task<Response> UpdateItemAsync(Guid productId, int quantity);
}
