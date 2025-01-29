namespace Sales.API.Application.DTOs;

public record ProductDTO(string Name, string Description, string ImageUrl, decimal Price, int QuantityInStock);
