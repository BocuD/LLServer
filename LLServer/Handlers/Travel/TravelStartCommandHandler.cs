using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers.Travel;

/*
  "param": {
    "character_id": 4,
    "m_travel_pamphlet_id": 201,
    "map_id": 201,
    "mas_max": 60,
    "slot": 0
  },
  "protocol": "TravelStart",
 */

public record TravelStartCommand(RequestBase request) : IRequest<ResponseContainer>;

public class TravelStartCommandHandler : IRequestHandler<TravelStartCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<TravelStartCommandHandler> logger;
    private readonly SessionHandler sessionHandler;

    public TravelStartCommandHandler(ApplicationDbContext dbContext, ILogger<TravelStartCommandHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }
    
    public async Task<ResponseContainer> Handle(TravelStartCommand command, CancellationToken cancellationToken)
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
                Response = new TravelStartResponse
                {
                    OtherPlayers = Array.Empty<TravelOtherPlayerData>(),
                }
            };
        }
        
        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        TravelStartParam? travelStart = JsonSerializer.Deserialize<TravelStartParam>(paramJson);
        if (travelStart is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);
        
        //make sure the traveldata for this slot exists
        TravelData? travelData = container.Travels.FirstOrDefault(x => x.Slot == travelStart.Slot);

        if (travelData is null)
        {
            container.Travels.Add(new TravelData
            {
                Slot = travelStart.Slot,
            });
            
            travelData = container.Travels.FirstOrDefault(x => x.Slot == travelStart.Slot);
        }

        if (travelData != null)
        {
            travelData.CharacterId = travelStart.CharacterId;
            travelData.TravelPamphletId = travelStart.TravelPamphletId;
        }
        
        //make sure the travel pamphlet exists
        TravelPamphlet? travelPamphlet = container.TravelPamphlets.FirstOrDefault(x => x.TravelPamphletId == travelStart.TravelPamphletId);

        if (travelPamphlet is null)
        {
            container.TravelPamphlets.Add(new TravelPamphlet
            {
                TravelPamphletId = travelStart.TravelPamphletId,
                IsNew = true,
                Round = 0,
                TotalDiceCount = 0,
                TotalTalkCount = 0,
                TravelExRewards = new int[0],
            });
        }
        
        //save changes
        await container.SaveChanges(cancellationToken);

        //todo: implement other player data to appear on map
        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelStartResponse
            {
                OtherPlayers = Array.Empty<TravelOtherPlayerData>(),
            }
        };
    }
}