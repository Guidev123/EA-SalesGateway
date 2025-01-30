﻿using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Cart.AddItem;

namespace Sales.API.Services.Cart;

public sealed class CartRestService(HttpClient client)
                  : Service, ICartRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<AddItemToCartResponse>> AddItemToCartAsync(AddItemToCartCommand cartItens)
    {
        var response = await _client.PostAsync("/api/v1/carts", GetContent(cartItens)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<AddItemToCartResponse>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response<CartDTO>> GetByCustomerIdAsync()
    {
        var response = await _client.GetAsync("/api/v1/carts").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<CartDTO>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response> ApplyVoucherToCartAsync(string voucherCode)
    {
        var response = await _client.PostAsync($"/api/v1/carts/apply-voucher/{voucherCode}", null).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response>(response);

        return result is not null
            ? new((int)response.StatusCode, result.Message, result.Errors)
            : new(400, "Something failed during the request.");
    }

    public async Task<Response> RemoveItemFromCartAsync(Guid productId)
    {
        var response = await _client.DeleteAsync($"/api/v1/carts/{productId}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response>(response);

        return result is not null
            ? new((int)response.StatusCode, result.Message, result.Errors)
            : new(400, "Something failed during the request.");
    }

    public async Task<Response> UpdateCartItemAsync(Guid productId, int quantity)
    {
        var response = await _client.PutAsync($"/api/v1/carts/{productId}/{quantity}", null).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response>(response);

        return result is not null
            ? new((int)response.StatusCode, result.Message, result.Errors)
            : new(400, "Something failed during the request.");
    }
}

