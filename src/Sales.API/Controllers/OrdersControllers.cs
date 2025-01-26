﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Sales.API.Controllers;

[Authorize]
[Route("api/v1/sales/order")]
public class OrdersControllers : MainController
{
}
