using Microsoft.AspNetCore.Mvc;
using Sales.API.Configurations;
using Sales.API.Services.Catalogs;

namespace Sales.API.Controllers;

[Route("api/v1/sales/catalog")]
public class CatalogController(ICatalogRestService catalogService)
           : MainController
{
    private readonly ICatalogRestService _catalogService = catalogService;

    [HttpGet("{id:guid}")]
    public async Task<IResult> GetByIdAsync(Guid id)
        => CustomResponse(await _catalogService.GetByIdAsync(id));

    [HttpGet]
    public async Task<IResult> GetAllAsync(int pageNumber = ApiConfiguration.DEFAULT_PAGE,
                                           int pageSize = ApiConfiguration.DEFAULT_PAGE_SIZE)
        => CustomResponse(await _catalogService.GetAllAsync(pageNumber, pageSize));
}
