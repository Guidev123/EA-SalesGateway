namespace Sales.API.Application.DTOs;

public record CreateOrderDTO(decimal TotalPrice, List<OrderItemDTO> OrderItems,
                                  string VoucherCode, bool VoucherIsUsed,
                                  decimal Discount, AddressDTO Address);

