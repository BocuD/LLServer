using System.Text;
using System.Text.Json;
using LLServer.Handlers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

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
        RequestBase? request;
        var bodyString = string.Empty;
        
        try
        {
            var buffer = new byte[Request.Body.Length];

            _ = await Request.Body.ReadAsync(buffer.AsMemory(0, (int)Request.Body.Length));

            bodyString = Encoding.UTF8.GetString(buffer).Replace("\0", "");
            request = JsonSerializer.Deserialize<RequestBase>(bodyString);
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, "Request deserialize failed");
            
            Logger.LogError("{Message}\n{StackTracee}\n{BodyString}", e.Message, e.StackTrace, bodyString);
            return BadRequest();
        }

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
            "auth"                => await mediator.Send(new AuthCommand(request.Param)),
            "gameentry"           => await mediator.Send(new GetGameEntryQuery(request)),
            "userdata.get"        => await mediator.Send(new GetUserDataQuery()),
            "userdata.initialize" => await mediator.Send(new InitializeUserDataCommand(request)),
            "userdata.set"        => await mediator.Send(new SetUserDataCommand(request.Param)),
            "checkword"           => await mediator.Send(new CheckWordCommand()),
            "ranking"             => await mediator.Send(new GetRankingQuery()),
            "gameresult"          => await mediator.Send(new GameResultCommand(request.Param)),
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