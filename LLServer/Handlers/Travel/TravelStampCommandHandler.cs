using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Travel;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers.Travel;

/*
{
    "param": {
        "travel_history_ids": [
            "1"
        ]
    },
    "protocol": "travelstamp",
}
 */

public record TravelStampCommand(RequestBase request) : BaseRequest(request);

public class TravelStampCommandHandler : ParamHandler<TravelStampParam, TravelStampCommand>
{
    public TravelStampCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<TravelStampParam, TravelStampCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    protected override async Task<ResponseContainer> HandleRequest(TravelStampParam param, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.UserData)
                .Include(u => u.UserDataAqours)
                .Include(u => u.UserDataSaintSnow)
                .Include(u => u.Members)
                .Include(u => u.MemberCards)
                .Include(u => u.LiveDatas)
                .Include(u => u.TravelData)
                .Include(u => u.TravelPamphlets)
                .Include(u => u.TravelHistory)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return new ResponseContainer
            {
                Result = 200,
                Response = new TravelStampResponse()
            };
        }
        
        //get persistent user data
        PersistentUserDataContainer container = new(dbContext, session);
        
        foreach(string id in param.TravelHistoryIds)
        {
            TravelHistory? history = container.TravelHistory.FirstOrDefault(x => x.Id == id);

            if (history != null)
            {
                //history.
                //probably do something with the travel history here
                //todo: figure out what to do with travel history
            }
        }

        List<TravelHistory> travelHistory;

        switch (container.UserData.IdolKind)
        {
            case 0:
                travelHistory = container.TravelHistory;
                break;
            case 1:
                travelHistory = container.TravelHistoryAqours;
                break;
            case 2:
                travelHistory = container.TravelHistorySaintSnow;
                break;
        }
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelStampResponse
            {
                TravelHistory = new TravelHistory[0]
            }
        };
    }
}
        