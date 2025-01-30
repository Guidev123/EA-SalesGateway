namespace Sales.API.Application.DTOs;

public record OrderItemDTO(Guid ProductId, string Name, decimal Price, string ImageUrl, int Quantity);
