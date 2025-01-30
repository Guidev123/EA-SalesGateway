﻿using MediatR;
using Sales.API.Application.Responses;

namespace Sales.API.Application.UseCases.Cart.AddItem;

public record AddItemToCartCommand(Guid ProductId, string Name, decimal Price, string ImageUrl, int Quantity)
    : IRequest<Response<AddItemToCartResponse>>;
