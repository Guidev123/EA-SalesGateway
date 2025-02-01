namespace Sales.API.Application.DTOs;

public record OrderDTO(string Code, bool VoucherIsUsed,
                       decimal Discount, decimal TotalPrice,
                       DateTime CreatedAt, int OrderStatus,
                       AddressDTO? Address, List<OrderItemDTO> OrderItems);
