using System.Text;
using System.Text.Json;
using LLServer.Database;
using LLServer.Handlers;
using LLServer.Handlers.Information;
using LLServer.Handlers.Terminal;
using LLServer.Handlers.Travel;
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
    private readonly bool detailedLogging = false;
    private readonly ApplicationDbContext dbContext;

    public GameController(IMediator mediator, IConfiguration configuration, ApplicationDbContext dbContext)
    {
        this.mediator = mediator;
        detailedLogging = configuration["DetailedLogging"] == "true";
        this.dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> BaseHandler()
    { 
        RequestBase? request;
        string bodyString = string.Empty;
        
        try
        {
            byte[] buffer = new byte[Request.Body.Length];

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
        
        Logger.LogInformation("Game request from {ip} Protocol: {Protocol}\nBody {Body}", Request.HttpContext.Connection.RemoteIpAddress, request.Protocol, bodyString);


        if (detailedLogging)
        {
            //for each successful request, log to the request log
            string fullRequest = $"{request.Protocol}\n{bodyString}\n";

            //write to text files
            await System.IO.File.AppendAllTextAsync("requests.log", fullRequest);
            await System.IO.File.AppendAllTextAsync("requests.log", "------------------------\n");
        }

        ResponseContainer response;
        
        try
        {
            response = request.Protocol switch
            {
                "unlock" => await mediator.Send(new UnlockQuery()),
                "gameconfig" => await mediator.Send(new GameConfigQuery()),
                "information" => await mediator.Send(new InformationQuery(Request.Host.Value)),
                "setterminallog" => await mediator.Send(new SetTerminalLogCommand(request)),
                
                "checkword" => await mediator.Send(new CheckWordCommand()),
                "ranking" => await mediator.Send(new GetRankingQuery()),

                "auth" => await mediator.Send(new AuthCommand(request.Param)),
                
                "userdata.get" => await mediator.Send(new GetUserDataQuery(request)),
                "userdata.initialize" => await mediator.Send(new InitializeUserDataCommand(request)),
                "userdata.set" => await mediator.Send(new SetUserDataCommand(request)),
                    
                "gameentry" => await mediator.Send(new GetGameEntryQuery(request)),
                "gameresult" => await mediator.Send(new GameResultCommand(request)),
                "gametotalresult" => await mediator.Send(new GameTotalResultQuery()),
                "gameexit" => await mediator.Send(new GameExitCommand(request)),
                
                "TravelStart" => await mediator.Send(new TravelStartCommand(request)),
                "TravelResult" => await mediator.Send(new TravelResultCommand(request)),
                "travelstamp" => await mediator.Send(new TravelStampCommand(request)),
                
                "music.unlock" => await mediator.Send(new MusicUnlockCommand(request)),
                
                "achievement" => await mediator.Send(new AchievementCommand(request)),
                "achievementyell" => await mediator.Send(new AchievementYellCommand(request)),
                _ => DefaultResponse(request.Protocol)
            };
        }
        catch (Exception e)
        {
            string fullRequest = $"{request.Protocol}\n{bodyString}\n";

            Logger.LogError(e, "Unhandled exception while handling request ({}): {Message}\n{StackTrace}\nOriginal request header: {fullRequest}", request.Protocol, e.Message, e.StackTrace, fullRequest);

            if (detailedLogging)
            {
                //log to request log
                await System.IO.File.AppendAllTextAsync("requests.log",
                    "Unhandled exception while handling request: " + fullRequest);
                await System.IO.File.AppendAllTextAsync("requests.log", e.Message);
                await System.IO.File.AppendAllTextAsync("requests.log", e.StackTrace);

                //log to response log
                await System.IO.File.AppendAllTextAsync("responses.log",
                    "Unhandled exception while handling request: " + fullRequest);
                await System.IO.File.AppendAllTextAsync("responses.log", e.Message);
                await System.IO.File.AppendAllTextAsync("responses.log", e.StackTrace);

                //log to the server log
                await System.IO.File.AppendAllTextAsync("server.log",
                    "Unhandled exception while handling request: " + fullRequest);
                await System.IO.File.AppendAllTextAsync("server.log", e.Message);
                await System.IO.File.AppendAllTextAsync("server.log", e.StackTrace);
            }

            return BadRequest();
        }


        if (detailedLogging)
        {
            string fullRequest = $"{request.Protocol}\n{bodyString}\n";
            
            //serialize the response using prettyprint
            string fullResponse =
                $"{response.Result}\n{JsonSerializer.Serialize(response.Response, new JsonSerializerOptions { WriteIndented = true })}\n";

            await System.IO.File.AppendAllTextAsync("responses.log", fullResponse);
            await System.IO.File.AppendAllTextAsync("responses.log", "------------------------\n");

            await System.IO.File.AppendAllTextAsync("server.log", fullRequest);
            await System.IO.File.AppendAllTextAsync("server.log", fullResponse);
            await System.IO.File.AppendAllTextAsync("server.log", "------------------------\n");
        }

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