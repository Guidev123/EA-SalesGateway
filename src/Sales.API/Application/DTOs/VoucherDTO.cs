namespace Sales.API.Application.DTOs;

public record VoucherDTO(decimal? Percentual, decimal? DiscountValue, string Code);
