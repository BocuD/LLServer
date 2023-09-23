using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Travel;
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

public class TravelStampCommandHandler : BaseHandler<TravelStampParam, TravelStampCommand>
{
    public TravelStampCommandHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<TravelStampParam, TravelStampCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    protected override async Task<ResponseContainer> HandleRequest(GameSession session, TravelStampParam param, CancellationToken cancellationToken)
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
            long idLong = long.Parse(id);
            
            //probably do something with the travel history here
            //todo: figure out what to do with travel history
            //container.TravelHistory.FirstOrDefault(x => x.Id == idLong)?.DoSomething();
        }

        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelStampResponse
            {
                TravelHistory = container.TravelHistory.ToArray()
            }
        };
    }
}
        