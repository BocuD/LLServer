using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Travel;
using LLServer.Models.UserData;
using LLServer.Session;
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
    private readonly SessionHandler sessionHandler;

    public TravelStampCommandHandler(ApplicationDbContext dbContext, ILogger<TravelStampCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(TravelStampCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        GameSession? session = await sessionHandler.GetSession(command.request, cancellationToken);

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

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        TravelStampParam? travelStamp = JsonSerializer.Deserialize<TravelStampParam>(paramJson);
        if (travelStamp is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get persistent user data
        PersistentUserDataContainer container = new(dbContext, session);
        
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
        