namespace Sales.API.DTOs;

public record CartItensDTO(Guid ProductId, string Name, string ImageUrl, decimal Price, int Quantity);
