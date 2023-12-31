using System.Text.Json;
using LLServer.Database;
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
    //data from several requests combined; an actual requests has most of this data missing
    "param": {
        "badges": [92004],
        "card_frames": [7518],
        "coop_player_ids": [1],
        "dice_count": 1,
        "get_memorial_cards": [
            {
                "location":900,
                "memorial_card_id":4002
            }
        ],
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
        "nameplates": [1810481],
        "release_pamphlet_ids": [],
        "special_ids": [4],
        "stage_ids": [106],
        "talk_count": 1,
        "tenpo_name": "LLServer",
        "total_exp": 28617,
        "travel_ex_rewards": [20370],
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
                .Include(u => u.MemorialCards)
                
                .Include(u => u.LiveDatas)
                
                .Include(u => u.TravelData)
                .Include(u => u.TravelPamphlets)
                .Include(u => u.TravelTalks)
                
                .Include(u => u.TravelHistory)
                
                .Include(u => u.Items)
                .Include(u => u.SpecialItems)
                
                .Include(u => u.NamePlates)
                .Include(u => u.Badges)
                .Include(u => u.Honors)
                .Include(u => u.CardFrames)
                
                .Include(u => u.MailBox)
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

        //release pamphlets
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
        */
        
        //travel talks
        foreach (TravelTalk travelTalk in travelResult.TravelTalks)
        {
            //add the talk as new if it doesn't exist
            //todo who the fuck knows if this is actually correct lmao
            TravelTalk? talk = container.TravelTalks.FirstOrDefault(t => 
                t.TalkId == travelTalk.TalkId && 
                t.MyCharacterId == travelTalk.MyCharacterId && 
                t.OtherCharacterId == travelTalk.OtherCharacterId);
            
            if (talk == null)
            {
                container.TravelTalks.Add(new TravelTalk
                {
                    TalkId = travelTalk.TalkId,
                    MyCharacterId = travelTalk.MyCharacterId,
                    OtherCharacterId = travelTalk.OtherCharacterId
                });
            }
        }
        
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
        
        //card frames
        foreach (int cardframe in travelResult.CardFrames)
        {
            //find matching card frame id in card frames, if it doesn't exist add a new card frame entry
            CardFrame? dataCardFrame = container.CardFrames.FirstOrDefault(c => c.CardFrameId == cardframe);
            if (dataCardFrame == null)
            {
                container.CardFrames.Add(new CardFrame
                {
                    CardFrameId = cardframe,
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
        

        //clear history above 100 entries per idol kind
        int historyCount = container.TravelHistory.Count(x => x.IdolKind == container.UserData.IdolKind);
        if (historyCount > 100)
        {
            TravelHistory oldestHistory = container.TravelHistory
                .Where(x => x.IdolKind == container.UserData.IdolKind)
                .OrderBy(x => x.Created)
                .First();
            
            container.TravelHistory.Remove(oldestHistory);
        }
        
        //record history
        foreach (TravelHistory_ toRecord in travelResult.TravelHistory)
        {
            TravelHistory newHistory = new()
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
                TravelPamphletId = travelResult.UserTravel.TravelPamphletId,
                TenpoName = travelResult.TenpoName,
                SnapBackgroundId = toRecord.SnapBackgroundId,
            };

            container.TravelHistory.Add(newHistory);
        }

        //build mailbox
        List<GetCardData> getCardDatas = new();

        void AddMailBoxCard(int attribute, int category, int count, int itemId, int location)
        {
            //add entry to getcarddata
            getCardDatas.Add(new GetCardData
            {
                Location = location,
                MailBoxId = (container.MailBox.Count + 1).ToString()
            });
            
            //add entry to mailbox
            container.MailBox.Add(new MailBoxItem
            {
                Attrib = attribute,
                Category = category,
                Count = count,
                Id = (container.MailBox.Count + 1).ToString(),
                ItemId = itemId
            });
        }
        
        //NOTE: the game seems to not like mailbox item 0, so we skip it
        
        //handle earned memorial cards
        foreach (GetMemorialCard memorialCard in travelResult.GetMemorialCards)
        {
            //category 6 for memorialcards
            AddMailBoxCard(0, 6, 1, memorialCard.MemorialCardId, memorialCard.Location);
        }

        //handle earned skill cards
        foreach (GetSkillCard skillCard in travelResult.GetSkillCards)
        {
            //category 2 for skill cards
            AddMailBoxCard(0, 2, 1, skillCard.SkillCardId, skillCard.Location);
        }

        //handle lot gachas
        foreach (LotGacha gacha in travelResult.LotGachas)
        {
            for (int i = 0; i < gacha.CardCount; i++)
            {
                // getCardDatas.Add(new GetCardData
                // {
                //     Location = gacha.Location,
                //     MailBoxId = mailBoxItems.Count.ToString()
                // });
                //
                // mailBoxItems.Add(new MailBoxItem
                // {
                //     Attrib = num,
                //     Category = num,
                //     Count = 1,
                //     Id = mailBoxItems.Count.ToString(),
                //     ItemId = 4306 + mailBoxItems.Count
                // });
            }
        }

        //save changes
        await container.SaveChanges(cancellationToken);
        
        //load new ids from db
        List<string> travelHistoryIds = dbContext.TravelHistory
            .Where(t => t.UserID == session.UserId)
            .Select(t => t.Id)
            .ToList();

        //log full contents of getcarddatas and mailboxitems
        logger.LogInformation("GetCardDatas: {GetCardDatas}\nMailBox: {MailBox}", JsonSerializer.Serialize(getCardDatas), JsonSerializer.Serialize(container.MailBox));

        return new ResponseContainer
        {
            Result = 200,
            Response = new TravelResultResponse
            {
                GetCardDatas = getCardDatas.ToArray(),
                TravelHistoryIds = travelHistoryIds.Select(x => x.ToString()).ToArray(),
                MailBox = container.MailBox.ToArray()
            }
        };
    }
}