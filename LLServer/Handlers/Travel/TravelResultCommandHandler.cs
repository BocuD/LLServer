using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
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
        "badges": [],
        "card_frames": [],
        "coop_player_ids": [1],
        "dice_count": 1,
        "get_memorial_cards": [],
        "get_skill_cards": [
            {
                "location": 1600,
                "skill_card_id": 4209
            }
        ],
        "item": [],
        "level": 30,
        "lot_gachas": [
            {
                "card_count": 1,
                "gacha_id": "gta_travel_member_106",
                "location": 500,
                "order": 0
            },
            {
                "card_count": 1,
                "gacha_id": "gta_travel_member_4",
                "location": 202,
                "order": 0
            }
        ],
        "m_card_member_id": 40011,
        "member_yell": [],
        "nameplates": [],
        "release_pamphlet_ids": [],
        "special_ids": [],
        "stage_ids": [],
        "talk_count": 1,
        "tenpo_name": "LLServer",
        "total_exp": 28617,
        "travel_ex_rewards": [],
        "travel_history": [
            {
                "create_type": 2,
                "m_snap_background_id": 20100,
                "other_character_id": 6,
                "other_d_user_id": 0
            }
        ],
        "travel_talks": [
            {
                "my_character_id": 4,
                "other_character_id": 6,
                "talk_id": 400006
            }
        ],
        "user_travel": {
            "character_id": 4,
            "is_goal": 0,
            "last_landmark": 9,
            "m_card_memorial_id": 4000,
            "m_travel_pamphlet_id": 201,
            "positions": [
                2,
                7,
                7
            ],
            "slot": 0
        },
        "walk_count": 5
    },
    "protocol": "TravelResult",
}
 */

public record TravelResultCommand(RequestBase request) : BaseRequest(request);

public class TravelResultCommandHandler : ParamHandler<TravelResultParam, TravelResultCommand>
{
    public TravelResultCommandHandler(ApplicationDbContext dbContext,
        ILogger<ParamHandler<TravelResultParam, TravelResultCommand>> logger, SessionHandler sessionHandler) : base(
        dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(TravelResultParam travelResult, CancellationToken cancellationToken)
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
                .Include(u => u.SkillCards)
                .Include(u => u.LiveDatas)
                
                .Include(u => u.TravelData)
                .Include(u => u.TravelPamphlets)
                
                .Include(u => u.TravelHistory)
                .Include(u => u.TravelHistoryAqours)
                .Include(u => u.TravelHistorySaintSnow)
                
                .Include(u => u.Items)
                .Include(u => u.SpecialItems)
                
                .Include(u => u.NamePlates)
                .Include(u => u.Badges)
                .Include(u => u.Honors)
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

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        //todo: figure out what the hell this stuff even is for
        /*
        "lot_gachas": [],
        "m_card_member_id": 40011,
        */

        foreach (int releasedId in travelResult.ReleasePamphletIds)
        {
            //add the pamphlet as new if it doesn't exist
            TravelPamphlet? pamphlet = container.TravelPamphlets.FirstOrDefault(p => p.TravelPamphletId == releasedId);

            if (pamphlet == null)
            {
                container.TravelPamphlets.Add(new TravelPamphlet
                {
                    TravelPamphletId = releasedId,
                    IsNew = true,
                    Round = 0
                });
            }
        }
        /*
        "travel_ex_rewards": [],
        "travel_talks": [],
        */

        //these are mainly not implemented because they are not stored in the database
        //todo: card frames
        //todo: coop player ids

        //badges
        foreach (int badge in travelResult.Badges)
        {
            //find matching batch id in badges, if it doesn't exist add a new badge entry
            Badge? dataBadge = container.Badges.FirstOrDefault(b => b.BadgeId == badge);
            if (dataBadge == null)
            {
                container.Badges.Add(new Badge
                {
                    BadgeId = badge,
                    New = true
                });
            }
        }

        //nameplates
        foreach (int nameplate in travelResult.Nameplates)
        {
            //find matching nameplate id in nameplates, if it doesn't exist add a new nameplate entry
            NamePlate? dataNameplate = container.NamePlates.FirstOrDefault(n => n.NamePlateId == nameplate);
            if (dataNameplate == null)
            {
                container.NamePlates.Add(new NamePlate
                {
                    NamePlateId = nameplate,
                    New = true
                });
            }
        }

        //items
        foreach (Item resultItem in travelResult.Item)
        {
            //find a matching item id in items, if it doesn't exist add a new item entry
            Item? dataItem = container.Items.FirstOrDefault(i => i.ItemId == resultItem.ItemId);
            if (dataItem == null)
            {
                container.Items.Add(new Item
                {
                    ItemId = resultItem.ItemId,
                });
                dataItem = container.Items.FirstOrDefault(i => i.ItemId == resultItem.ItemId);
            }

            if (dataItem != null)
            {
                dataItem.Count = resultItem.Count;
            }
        }

        //special ids: add new special items; only the new items are sent in the request
        foreach (int specialId in travelResult.SpecialIds)
        {
            container.SpecialItems.Add(new SpecialItem
            {
                IdolKind = container.UserData.IdolKind,
                SpecialId = specialId,
            });
        }

        //make sure we don't have more than 3 special items per idol kind
        for (int idolKind = 0; idolKind < 3; idolKind++)
        {
            //get all special items for this idol kind
            List<SpecialItem> specialItems = container.SpecialItems.Where(s => s.IdolKind == idolKind).ToList();

            //if we have more than 3, remove the oldest ones
            if (specialItems.Count > 3)
            {
                //create list of items from front of list to remove from SpecialItems list
                List<SpecialItem> itemsToRemove = specialItems.Take(specialItems.Count - 3).ToList();

                //remove items
                foreach (SpecialItem item in itemsToRemove)
                {
                    container.SpecialItems.Remove(item);
                }
            }
        }

        //todo: stage ids
        //todo: earned memorial cards

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
                container.Members.Add(new MemberData
                {
                    CharacterId = memberYell.CharacterId,
                });
                member = container.Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
            }

            if (member != null)
            {
                member.YellPoint = memberYell.YellPoint;
            }
        }

        //update travel pamphlet
        TravelPamphlet? travelPamphlet =
            container.TravelPamphlets.FirstOrDefault(
                t => t.TravelPamphletId == travelResult.UserTravel.TravelPamphletId);

        if (travelPamphlet is null)
        {
            container.TravelPamphlets.Add(new TravelPamphlet
            {
                TravelPamphletId = travelResult.UserTravel.TravelPamphletId,
            });
            travelPamphlet =
                container.TravelPamphlets.FirstOrDefault(t =>
                    t.TravelPamphletId == travelResult.UserTravel.TravelPamphletId);
        }

        if (travelPamphlet != null)
        {
            travelPamphlet.TotalDiceCount += travelResult.DiceCount;
            travelPamphlet.TotalTalkCount += travelResult.TalkCount;
            travelPamphlet.IsNew = false;

            if (travelResult.UserTravel.IsGoal == 1)
            {
                travelPamphlet.Round++;
            }
        }

        //save traveldata
        TravelData? travelData = container.Travels.FirstOrDefault(t => t.Slot == travelResult.UserTravel.Slot);

        if (travelData is null)
        {
            container.Travels.Add(new TravelData
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

        List<long> travelHistoryIds = new();

        foreach (TravelHistory_ toRecord in travelResult.TravelHistory)
        {
            TravelHistoryBase newHistory = new()
            {
                CardMemberId = travelResult.CardMemberId,
                Created = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"),
                CreateType = toRecord.CreateType,
                OtherCharacterId = toRecord.OtherCharacterId,

                //todo: store a player id in the database instead of all the separate attributes
                //adding some dummy stuff here for now
                OtherPlayerName = "園田海未",
                OtherPlayerBadge = 901001,
                OtherPlayerNameplate = 19001,
                Id = highestTravelId + 1,
                TravelPamphletId = travelResult.UserTravel.TravelPamphletId,
                TenpoName = travelResult.TenpoName,
                SnapBackgroundId = toRecord.SnapBackgroundId,
            };

            travelHistoryIds.Add(newHistory.Id);
            highestTravelId += 1;

            //todo: move away from reflectionmapper here because it is inefficient
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
                    container.TravelHistorySaintSnow.Add(ReflectionMapper.Map(newHistory,
                        new TravelHistorySaintSnow()));
                    break;
            }
        }

        List<GetCardData> getCardDatas = new();
        List<MailBoxItem> mailBoxItems = new();
        
        //handle earned skill cards
        foreach (GetSkillCard skillCard in travelResult.GetSkillCards)
        {
            //add entry to getcarddata
            getCardDatas.Add(new GetCardData
            {
                Location = skillCard.Location,
                MailBoxId = mailBoxItems.Count
            });
            
            //add entry to mailbox
            mailBoxItems.Add(new MailBoxItem
            {
                Attrib = 1,
                Category = 1,
                Count = 1,
                Id = mailBoxItems.Count,
                ItemId = skillCard.SkillCardId
            });
            
            //add the card to the database
            SkillCardData? skillCardData = container.SkillCards.FirstOrDefault(s => s.CardSkillId == skillCard.SkillCardId);

            if (skillCardData == null)
            {
                container.SkillCards.Add(new SkillCardData
                {
                    CardSkillId = skillCard.SkillCardId,
                    SkillLevel = 1,
                    New = true,
                    PrintRest = 1
                });
            }
        }
        
        //handle lot gachas
        foreach (LotGacha gacha in travelResult.LotGachas)
        {
            for (int i = 0; i < gacha.CardCount; i++)
            {
                getCardDatas.Add(new GetCardData
                {
                    Location = gacha.Location,
                    MailBoxId = mailBoxItems.Count
                });
                
                mailBoxItems.Add(new MailBoxItem
                {
                    Attrib = 2,
                    Category = 3,
                    Count = 1,
                    Id = mailBoxItems.Count,
                    ItemId = 40071
                });
            }
        }

        //save changes
        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelResultResponse
            {
                GetCardDatas = getCardDatas.ToArray(),
                TravelHistoryIds = travelHistoryIds.Select(x => x.ToString()).ToArray(),
                MailBox = mailBoxItems.ToArray()
            }
        };
    }
}