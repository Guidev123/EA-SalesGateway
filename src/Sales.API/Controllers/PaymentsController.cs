using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/payment")]
public class PaymentsController : MainController
{

}
