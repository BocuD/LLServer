using System.Text;
using System.Text.Json;
using LLServer.Common;
using LLServer.Handlers;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.UserDataModel;
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

        ResponseContainer response;
        switch (request.Protocol)
        {
            case "unlock":
                response = await mediator.Send(new UnlockQuery());
                break;
            
            case "gameconfig":
                response = await mediator.Send(new GameConfigQuery());
                break;
            
            case "information":
                response = await mediator.Send(new InformationQuery(Request.Host.Value));
                break;
            
            case "auth":
                response = await mediator.Send(new AuthCommand());
                break;
            
            case "userdata.initialize":
            {
                if (request.Param == null)
                {
                    response = StaticResponses.BadRequestResponse;
                    break;
                }
                //deserialize from param
                var paramJson = request.Param.Value.GetRawText();

                response = await mediator.Send(new InitializeUserDataCommand(paramJson));
            }
                break;

            case "gameentry":
                response = await mediator.Send(new GetGameEntryQuery());
                break;
            
            case "userdata.get":
                response = await mediator.Send(new GetUserDataQuery());
                break;
            
            case "userdata.set":
            {
                if (request.Param == null) 
                {
                    response = StaticResponses.BadRequestResponse;
                    break;
                }
                
                var paramJson = request.Param.Value.GetRawText();
                
                Logger.LogInformation("ParamJson {ParamJson}", paramJson);

                response = await mediator.Send(new SetUserDataCommand(paramJson));
            }
                break;
            case "checkword":
                response = await mediator.Send(new CheckWordCommand());
                break;
            
            case "ranking":
                response = await mediator.Send(new GetRankingQuery());
                break;
            
            default:
                Logger.LogWarning("Unhandled protocol: {Protocol}", request.Protocol);
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
        }

        return Ok(response);
    }
}