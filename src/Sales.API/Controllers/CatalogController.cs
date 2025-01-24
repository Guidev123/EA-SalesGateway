using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/catalog")]
public class CatalogController : MainController
{
}
