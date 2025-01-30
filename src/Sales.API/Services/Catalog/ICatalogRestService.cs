using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Catalog;

public interface ICatalogRestService
{
    Task<Response<ProductDTO>> GetByIdAsync(Guid id);
    Task<PagedResponse<IEnumerable<ProductDTO>>> GetAllAsync();
}
