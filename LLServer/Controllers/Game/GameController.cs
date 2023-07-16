using System.Text;
using System.Text.Json;
using LLServer.Handlers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Game;

[ApiController]
[Route("game")]
public class GameController : BaseController<GameController>
{
    private readonly IMediator mediator;

    public GameController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> BaseHandler()
    {
        var body = await Request.BodyReader.ReadAsync();
        var bodyString = Encoding.Default.GetString(body.Buffer).Replace("\0", string.Empty);

        // Parse body as json
        var request = JsonSerializer.Deserialize<RequestBase>(bodyString);
        if (request is null)
        {
            Logger.LogWarning("Request deserialize failed");
            return BadRequest();
        }

        Logger.LogInformation("Protocol: {Protocol}\nBody {Body}", request.Protocol, bodyString);

        var response = request.Protocol switch
        {
            "unlock"              => await mediator.Send(new UnlockQuery()),
            "gameconfig"          => await mediator.Send(new GameConfigQuery()),
            "information"         => await mediator.Send(new InformationQuery(Request.Host.Value)),
            "auth"                => await mediator.Send(new AuthCommand()),
            "gameentry"           => await mediator.Send(new GetGameEntryQuery()),
            "userdata.get"        => await mediator.Send(new GetUserDataQuery()),
            "userdata.initialize" => await mediator.Send(new InitializeUserDataCommand(request.Param)),
            "userdata.set"        => await mediator.Send(new SetUserDataCommand(request.Param)),
            "checkword"           => await mediator.Send(new CheckWordCommand()),
            "ranking"             => await mediator.Send(new GetRankingQuery()),
            _                     => DefaultResponse(request.Protocol)
        };

        return Ok(response);
    }

    private ResponseContainer DefaultResponse(string protocol)
    {
        Logger.LogWarning("Unhandled protocol: {Protocol}", protocol);
        return new ResponseContainer
        {
            Result = 200,
            Response = new ResponseBase()
        };
    }
}