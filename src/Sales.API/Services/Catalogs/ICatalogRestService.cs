using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;

namespace Sales.API.Services.Catalogs;

public interface ICatalogRestService
{
    Task<Response<ProductDTO>> GetByIdAsync(Guid id);
    Task<PagedResponse<IEnumerable<ProductDTO>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Response<IEnumerable<OrderItemDTO>>> GetByIdsAsync(IEnumerable<Guid> ids);
}
