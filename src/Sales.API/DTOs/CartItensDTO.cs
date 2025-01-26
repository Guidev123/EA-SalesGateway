namespace Sales.API.DTOs;

public record CartItensDTO(Guid ProductId, string Name, decimal Price, string Image, int Quantity);
