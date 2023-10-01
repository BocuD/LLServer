using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameExitCommand(RequestBase request) : BaseRequest(request);

//from what i can see so far, the game sends giant arrays with ids of items / lives / etc that was seen during a session
//for each of these items new status should be cleared

/*
{
    "param": {
        "achievements": [],
        "badges": [],
        "flags": "00000000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000",
        "honors": [],
        "limited_achievements": [],
        "lives": [
            10000,
            10001,
            10002,
            <etc>
        ],
        "membercard": [],
        "members": [],
        "memorialcard": [],
        "musics": [
            10,
            20,
            30,
            <etc>
        ],
        "nameplates": [],
        "skillcard": [],
        "stages": []
    },
    "protocol": "gameexit",
}
 */

public class GameExitCommandHandler : ParamHandler<GameExitParam, GameExitCommand>
{
    public GameExitCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<GameExitParam, GameExitCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameExitParam gameResult, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Achievements)
                .Include(u => u.Badges)
                .Include(u => u.Honors)
                //.Include(u => u.LimitedAchievements)
                .Include(u => u.LiveDatas)
                .Include(u => u.MemberCards)
                .Include(u => u.Members)
                .Include(u => u.MemorialCards)
                .Include(u => u.Musics)
                .Include(u => u.NamePlates)
                .Include(u => u.SkillCards)
                //.Include(u => u.Stages)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        //update achievements
        foreach (int achievement in gameResult.Achievements)
        {
            Achievement? existingAchievement = container.Achievements.FirstOrDefault(a => a.AchievementId == achievement);

            if (existingAchievement == null)
            {
                container.Achievements.Add(new Achievement
                {
                    AchievementId = achievement,
                    New = false,
                    Unlocked = true
                });
            }
            else
            {
                existingAchievement.New = false;
                existingAchievement.Unlocked = true;
            }
        }
        
        //update badges
        foreach (int badge in gameResult.Badges)
        {
            Badge? existingBadge = container.Badges.FirstOrDefault(b => b.BadgeId == badge);
            
            if (existingBadge == null)
            {
                container.Badges.Add(new Badge
                {
                    BadgeId = badge,
                    New = false
                });
            }
            else
            {
                existingBadge.New = false;
            }
        }

        //update flags
        container.Flags = gameResult.Flags;
        
        //update honors
        foreach (int honor in gameResult.Honors)
        {
            HonorData? existingHonor = container.Honors.FirstOrDefault(h => h.HonorId == honor);
            
            if (existingHonor == null)
            {
                container.Honors.Add(new HonorData
                {
                    HonorId = honor,
                    New = false,
                    Unlocked = true
                });
            }
            else
            {
                existingHonor.New = false;
                existingHonor.Unlocked = true;
            }
        }
        
        //update limited achievements
        foreach (int limitedAchievement in gameResult.LimitedAchievements)
        {
            LimitedAchievement? existingLimitedAchievement = container.LimitedAchievements.FirstOrDefault(la => la.LimitedAchievementId == limitedAchievement);

            if (existingLimitedAchievement == null)
            {
                container.LimitedAchievements.Add(new LimitedAchievement
                {
                    LimitedAchievementId = limitedAchievement,
                    New = false,
                    Unlocked = true
                });
            }
            else
            {
                existingLimitedAchievement.New = false;
                existingLimitedAchievement.Unlocked = true;
            }
        }

        //update lives
        foreach (int liveId in gameResult.Lives)
        {
            PersistentLiveData? existingLive = container.PersistentLives.FirstOrDefault(l => l.LiveId == liveId);

            if (existingLive == null)
            {
                container.PersistentLives.Add(new PersistentLiveData
                {
                    LiveId = liveId,
                    New = false,
                    Unlocked = true
                });
            }
            else
            {
                existingLive.New = false;
                existingLive.Unlocked = true;
            }
        }
        
        //update member cards
        foreach (int membercard in gameResult.MemberCards)
        {
            //try to get an existing entry for the card
            MemberCardData? existingEntry = container.MemberCards.FirstOrDefault(c => c.CardMemberId == membercard);
            if (existingEntry == null)
            {
                //create a new entry if one doesn't exist yet
                container.MemberCards.Add(new MemberCardData
                {
                    CardMemberId = membercard,
                    Count = 1,
                    New = false
                });
            }
            else
            {
                existingEntry.New = false;
            }
        }
        
        //update members
        foreach (int member in gameResult.Members)
        {
            MemberData? existingMember = container.Members.FirstOrDefault(m => m.CharacterId == member);
            if (existingMember == null)
            {
                //we fucked up, this shouldn't happen lol
                container.Members.Add(new MemberData
                {
                    CharacterId = member,
                    New = false
                });
            }
            else
            {
                existingMember.New = false;
            }
        }
        
        //update memorial cards
        foreach (int memorialCard in gameResult.MemorialCards)
        {
            MemorialCardData? existingCard =
                container.MemorialCards.FirstOrDefault(c => c.CardMemorialId == memorialCard);
            if (existingCard == null)
            {
                container.MemorialCards.Add(new MemorialCardData
                {
                    CardMemorialId = memorialCard,
                    New = false
                });
            }
            else
            {
                existingCard.New = false;
            }
        }
        
        //update musics
        foreach (int music in gameResult.Musics)
        {
            MusicData? existingMusic = container.Musics.FirstOrDefault(m => m.MusicId == music);
            if (existingMusic == null)
            {
                container.Musics.Add(new MusicData
                {
                    MusicId = music,
                    New = false,
                    Unlocked = true
                });
            }
            else
            {
                existingMusic.New = false;
            }
        }
        
        //update name plates
        foreach (int nameplate in gameResult.Nameplates)
        {
            NamePlate? existingNameplate = container.NamePlates.FirstOrDefault(s => s.NamePlateId == nameplate);
            if (existingNameplate == null)
            {
                container.NamePlates.Add(new NamePlate
                {
                    NamePlateId = nameplate,
                    New = false
                });
            }
            else
            {
                existingNameplate.New = false;
            }
        }
        
        //update skill cards
        foreach (int skillcard in gameResult.SkillCards)
        {
            SkillCardData? existingSkillCard = container.SkillCards.FirstOrDefault(s => s.CardSkillId == skillcard);
            if (existingSkillCard == null)
            {
                container.SkillCards.Add(new SkillCardData
                {
                    CardSkillId = skillcard,
                    New = false
                });
            }
            else
            {
                existingSkillCard.New = false;
            }
        }
        
        //todo update stages
        
        //remove session
        dbContext.Sessions.Remove(session);
        
        //write to database
        await container.SaveChanges(cancellationToken);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new ResponseBase()
        };
    }
}