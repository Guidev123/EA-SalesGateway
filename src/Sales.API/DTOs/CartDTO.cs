namespace Sales.API.DTOs;

public record CartDTO(decimal TotalPrice, string? VoucherCode,
                      bool VoucherIsUsed, decimal Discount,
                      List<CartItensDTO> OrderItems);
