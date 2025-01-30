using Sales.API.Application.UseCases.Cart.AddItem;

namespace Sales.API.Application.DTOs;

public record CartDTO(decimal TotalPrice, string? VoucherCode,
                      bool VoucherIsUsed, decimal? Discount,
                      List<AddItemToCartCommand> CartItems);
