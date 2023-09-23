using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Terminal;
using LLServer.Models.Responses.Travel;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers.Terminal;

/*
{
    "blockseq": 5,
    "game": {
        "eventcode": "000",
        "version": "2.4.1"
    },
    "param": {
        "anime_type": 2,
        "dice_bonus": 0,
        "m_snap_frame_id": 101,
        "motion_frame": 54,
        "travel_history_id": "0"
    },
    "protocol": "TravelSnap.print",
    "sessionkey": "d9fd51f248da4e2c80d678b5ac17776c",
    "terminal": {
        "tenpo_id": "1337",
        "tenpo_index": 1337,
        "terminal_attrib": 1,
        "terminal_id": "0CC47A92BC01"
    }
}
 */

public record TravelSnapPrintCommand(RequestBase request) : IRequest<ResponseContainer>;

public class TravelSnapPrintCommandHandler : IRequestHandler<TravelSnapPrintCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<TravelSnapPrintCommandHandler> logger;
    private readonly SessionHandler sessionHandler;

    public TravelSnapPrintCommandHandler(ApplicationDbContext dbContext, ILogger<TravelSnapPrintCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }
    
    public async Task<ResponseContainer> Handle(TravelSnapPrintCommand printCommand, CancellationToken cancellationToken)
    {
        if (printCommand.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        GameSession? session = await sessionHandler.GetSession(printCommand.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.UserData)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return new ResponseContainer
            {
                Result = 200,
                Response = new TravelResultResponse()
            };
        }

        string paramJson = printCommand.request.Param.Value.GetRawText();

        //get travel snap print
        TravelSnapPrintCommand? travelResult = JsonSerializer.Deserialize<TravelSnapPrintCommand>(paramJson);
        if (travelResult is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        return new ResponseContainer()
        {
            Result = 200,
            Response = new TravelPrintResponse()
            {
                TravelSnapId = "000102030405060708090a0b0c0d0e0f",
                Created = DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss"),
            }
        };
    }
}