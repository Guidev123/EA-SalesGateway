using Sales.API.Application.Responses;
using Sales.API.DTOs;

namespace Sales.API.Services.Catalog;

public interface ICatalogRestService
{
    Task<Response<ProductDTO>> GetProductByIdAsync(Guid id);
}
