using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : BaseController<TestController>
{
    [HttpGet]
    public ActionResult<DateTime> GetDate()
    {
        return DateTime.Now;
    }
}