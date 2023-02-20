using System.Text;
using LLServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Game;

[ApiController]
[Route("game/info")]
public class InformationController : BaseController<InformationController>
{
    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> BaseHandler()
    {
        var body = await Request.BodyReader.ReadAsync();
        Logger.LogInformation("Body {Body}", Encoding.Default.GetString(body.Buffer));

        var response = new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = "http://127.0.0.1/game/next",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd")
            }
        };

        return Ok(response);
    }
}