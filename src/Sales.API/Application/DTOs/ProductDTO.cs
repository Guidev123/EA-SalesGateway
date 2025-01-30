namespace Sales.API.Application.DTOs;

using System.Text.Json.Serialization;

public record ProductDTO(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("imageUrl")] string ImageUrl,
    [property: JsonPropertyName("price")] decimal Price,
    [property: JsonPropertyName("quantityInStock")] int QuantityInStock
);
