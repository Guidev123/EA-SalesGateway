using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/payment")]
public class PaymentsController : MainController
{

}
