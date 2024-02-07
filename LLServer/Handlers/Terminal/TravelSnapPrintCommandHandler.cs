using System.Text.Json;
using LLServer.Common;
using LLServer.Database.Models;
using LLServer.Models.UserData;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Terminal;
using LLServer.Models.Responses.Travel;
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

public record TravelSnapPrintCommand(RequestBase request) : BaseRequest(request);

public class TravelSnapPrintCommandHandler : ParamHandler<EmptyParam, TravelSnapPrintCommand>
{
    public TravelSnapPrintCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<EmptyParam, TravelSnapPrintCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    protected override async Task<ResponseContainer> HandleRequest(EmptyParam param, TravelSnapPrintCommand request,
        CancellationToken cancellationToken)
    {
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
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelPrintResponse
            {
                TravelSnapId = "000102030405060708090a0b0c0d0e0f",
                Created = DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss"),
            }
        };
    }
}
