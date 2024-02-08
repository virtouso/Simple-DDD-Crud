using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.Controllers;


[ApiController]
[ApiVersion("1.0")]
public class CustomerController : Controller
{
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpPost]
    public IActionResult Index()
    {
      return   Ok();
    }
}