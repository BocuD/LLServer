using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
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
        "badges": [],
        "card_frames": [],
        "coop_player_ids": [],
        "dice_count": 1,
        "get_memorial_cards": [],
        "get_skill_cards": [],
        "item": [
            {
                "count": 1,
                "m_item_id": 20102
            }
        ],
        "level": 9,
        "lot_gachas": [],
        "m_card_member_id": 40011,
        "member_yell": [],
        "nameplates": [],
        "release_pamphlet_ids": [],
        "special_ids": [
            1
        ],
        "stage_ids": [],
        "talk_count": 0,
        "tenpo_name": "LLServer",
        "total_exp": 2223,
        "travel_ex_rewards": [],
        "travel_history": [
            {
                "create_type": 0,
                "m_snap_background_id": 20100,
                "other_character_id": 0,
                "other_d_user_id": 0
            }
        ],
        "travel_talks": [],
        "user_travel": {
            "character_id": 4,
            "is_goal": 0,
            "last_landmark": 2,
            "m_card_memorial_id": 4000,
            "m_travel_pamphlet_id": 201,
            "positions": [
                43
            ],
            "slot": 0
        },
        "walk_count": 5
    },
    "protocol": "TravelResult",
}
 */

public record TravelResultCommand(RequestBase request) : IRequest<ResponseContainer>;

public class TravelResultCommandHandler : IRequestHandler<TravelResultCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<TravelResultCommandHandler> logger;

    public TravelResultCommandHandler(ApplicationDbContext dbContext, ILogger<TravelResultCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(TravelResultCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get session
        Session? session = await dbContext.Sessions
            .Include(s => s.User)
            .Include(s => s.User.UserData)
            .Include(s => s.User.UserDataAqours)
            .Include(s => s.User.UserDataSaintSnow)
            .Include(s => s.User.Members)
            .Include(s => s.User.LiveDatas)
            .Include(s => s.User.TravelData)
            .Include(s => s.User.TravelPamphlets)
            .Include(s => s.User.TravelHistory)
            .Include(s => s.User.TravelHistoryAqours)
            .Include(s => s.User.TravelHistorySaintSnow)
            .FirstOrDefaultAsync(s => s.SessionId == command.request.SessionKey,
                cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        TravelResultParam? travelResult = JsonSerializer.Deserialize<TravelResultParam>(paramJson);
        if (travelResult is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        
        //todo: figure out what the hell this stuff even is for
        /*
        "lot_gachas": [],
        "m_card_member_id": 40011,
        "release_pamphlet_ids": [],
        "travel_ex_rewards": [],
        "travel_talks": [],
        "walk_count": 5
        */
        
        //these are mainly not implemented because they are not stored in the database
        //todo: badges
        //todo: card frames
        //todo: coop player ids
        //todo: earned items
        //todo: earned special ids
        //todo: stage ids
        //todo: earned nameplayes
        //todo: earned skill cards
        //todo: earned memorial cards
        //todo: record travel history

        //update level and exp
        container.UserData.Level = travelResult.Level;
        container.UserData.TotalExp = travelResult.TotalExp;
        
        //update member yell data
        foreach (MemberYell memberYell in travelResult.MemberYells)
        {
            //find a matching character id in members, if it doesn't exist add a new member entry
            MemberData? member = container.Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
            if (member == null)
            {
                container.Members.Add(new MemberData()
                {
                    CharacterId = memberYell.CharacterId,
                });
                member = container.Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
            }

            if (member != null)
            {
                member.YellPoint = memberYell.YellPoint;
                member.AchieveRank = memberYell.YellRank;
            }
        }

        //update travel pamphlet
        TravelPamphlet? travelPamphlet = container.TravelPamphlets.FirstOrDefault(t => t.Id == travelResult.UserTravel.TravelPamphletId);

        if (travelPamphlet is null)
        {
            container.TravelPamphlets.Add(new TravelPamphlet()
            {
                TravelPamphletId = travelResult.UserTravel.TravelPamphletId,
                IsNew = true
            });
            travelPamphlet = container.TravelPamphlets.FirstOrDefault(t => t.Id == travelResult.UserTravel.TravelPamphletId);
        }

        if (travelPamphlet != null)
        {
            travelPamphlet.TotalDiceCount += travelResult.DiceCount;
            travelPamphlet.TotalTalkCount += travelResult.TalkCount;
            travelPamphlet.Round += 1;
        }

        //save traveldata
        TravelData? travelData = container.Travels.FirstOrDefault(t => t.Slot == travelResult.UserTravel.Slot);
        
        if(travelData is null)
        {
            container.Travels.Add(new TravelData()
            {
                Slot = travelResult.UserTravel.Slot
            });
            
            travelData = container.Travels.FirstOrDefault(x => x.Slot == travelResult.UserTravel.Slot);
        }

        if (travelData != null)
        {
            travelData.TravelPamphletId = travelResult.UserTravel.TravelPamphletId;
            travelData.CharacterId = travelResult.UserTravel.CharacterId;
            travelData.CardMemorialId = travelResult.UserTravel.CardMemorialId;
            travelData.LastLandmark = travelResult.UserTravel.LastLandmark;
            travelData.Positions = travelResult.UserTravel.Positions.ToArray();
            travelData.Modified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //record travel history
        long highestTravelId = container.TravelHistory.Count > 0 ? container.TravelHistory.Max(x => x.Id) : 0;

        List<long> travelHistoryIds = new List<long>();
        
        foreach (TravelHistory_ toRecord in travelResult.TravelHistory)
        {
            TravelHistoryBase newHistory = new()
            {
                CardMemberId = travelResult.CardMemberId,
                Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateType = toRecord.CreateType,
                OtherCharacterId = toRecord.OtherCharacterId,

                //todo: store a player id in the database instead of all the separate attributes
                //adding some dummy stuff here for now
                OtherPlayerName = "園田海未",
                OtherPlayerBadge = 901001,
                OtherPlayerNameplate = 19001,
                Id = highestTravelId + 1,
            };
            
            travelHistoryIds.Add(newHistory.Id);
            highestTravelId += 1;

            switch (container.UserData.IdolKind)
            {
                //µ's
                case 0:
                    container.TravelHistory.Add(ReflectionMapper.Map(newHistory, new TravelHistory()));
                    break;
                
                //aqours
                case 1:
                    container.TravelHistoryAqours.Add(ReflectionMapper.Map(newHistory, new TravelHistoryAqours()));
                    break;
                
                //saint snow
                case 2:
                    container.TravelHistorySaintSnow.Add(ReflectionMapper.Map(newHistory, new TravelHistorySaintSnow()));
                    break;
            }
        }
        
        //save changes
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseContainer()
        {
            Result = 200,
            Response = new TravelResultResponse()
            {
                GetCardDatas = Array.Empty<GetCardData>(),
                TravelHistoryIds = travelHistoryIds.Select(x => x.ToString()).ToArray(),
                MailBox = Array.Empty<MailBoxItem>()
            }
        };
    }
}