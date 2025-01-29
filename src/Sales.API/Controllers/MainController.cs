using Microsoft.AspNetCore.Mvc;
using Sales.API.Application.Responses;

namespace Sales.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IResult CustomResponse<T>(Response<T> response) => response.StatusCode switch
    {
        200 => TypedResults.Ok(response),
        400 => TypedResults.BadRequest(response),
        201 => TypedResults.Created(string.Empty, response),
        204 => TypedResults.NoContent(),
        _ => TypedResults.NotFound()
    };

    [ApiExplorerSettings(IgnoreApi = true)]
    public IResult CustomResponse(Response response) => response.StatusCode switch
    {
        200 => TypedResults.Ok(response),
        400 => TypedResults.BadRequest(response),
        201 => TypedResults.Created(string.Empty, response),
        204 => TypedResults.NoContent(),
        _ => TypedResults.NotFound()
    };
}
