using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Catalog;

public interface ICatalogRestService
{
    Task<Response<ProductDTO>> GetProductByIdAsync(Guid id);
}
