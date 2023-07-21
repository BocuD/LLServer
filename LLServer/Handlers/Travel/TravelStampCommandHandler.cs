using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Travel;
using LLServer.Models.UserData;
using MediatR;
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

public record TravelStampCommand(RequestBase request) : IRequest<ResponseContainer>;

public class TravelStampCommandHandler : IRequestHandler<TravelStampCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<TravelStampCommandHandler> logger;

    public TravelStampCommandHandler(ApplicationDbContext dbContext, ILogger<TravelStampCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(TravelStampCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get session
        var session = await dbContext.Sessions
            .AsSplitQuery()
            .Where(s => s.SessionId == command.request.SessionKey)
            .Select(s => new
            {
                Session = s,
                User = s.User,
                UserData = s.User.UserData,
                UserDataAqours = s.User.UserDataAqours,
                UserDataSaintSnow = s.User.UserDataSaintSnow,
                Members = s.User.Members,
                MemberCards = s.User.MemberCards,
                LiveDatas = s.User.LiveDatas,
                TravelData = s.User.TravelData,
                TravelPamphlets = s.User.TravelPamphlets
            }).FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        TravelStampParam? travelStamp = JsonSerializer.Deserialize<TravelStampParam>(paramJson);
        if (travelStamp is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get persistent user data
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        foreach(string id in travelStamp.TravelHistoryIds)
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
        