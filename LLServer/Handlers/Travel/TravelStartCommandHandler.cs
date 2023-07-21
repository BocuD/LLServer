using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
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

    public TravelStartCommandHandler(ApplicationDbContext dbContext, ILogger<TravelStartCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }
    
    public async Task<ResponseContainer> Handle(TravelStartCommand command, CancellationToken cancellationToken)
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
        TravelStartParam? travelStart = JsonSerializer.Deserialize<TravelStartParam>(paramJson);
        if (travelStart is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        //make sure the traveldata for this slot exists
        TravelData? travelData = container.Travels.FirstOrDefault(x => x.Slot == travelStart.Slot);

        if (travelData is null)
        {
            container.Travels.Add(new TravelData()
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
        TravelPamphlet? travelPamphlet = container.TravelPamphlets.FirstOrDefault(x => x.Id == travelStart.TravelPamphletId);

        if (travelPamphlet is null)
        {
            container.TravelPamphlets.Add(new TravelPamphlet()
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
        await dbContext.SaveChangesAsync(cancellationToken);

        //todo: implement other player data to appear on map
        return new ResponseContainer()
        {
            Result = 200,
            Response = new TravelStartResponse()
            {
                OtherPlayers = new TravelOtherPlayerData[0],
            }
        };
    }
}