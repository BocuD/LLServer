using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record AchievementCommand(RequestBase request) : BaseRequest(request);

public class AchievementCommandHandler : ParamHandler<AchievementParam, AchievementCommand>
{
    public AchievementCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<AchievementParam, AchievementCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    protected override async Task<ResponseContainer> HandleRequest(AchievementParam achievementData,
        AchievementCommand request, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.AchievementRecordBooks)
                .Include(u => u.Achievements)
                .Include(u => u.LimitedAchievements)
                .Include(u => u.Items)
                .Include(u => u.Honors)
                //.Include(u => u.Stages)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);
        
        //update achievement data
        //todo: couple achievement rewards (such as nameplates, badges, etc) with the achievement data
        foreach (int achievementId in achievementData.Achievements)
        {
            Achievement? achievement = container.Achievements.FirstOrDefault(a => a.AchievementId == achievementId);
            if (achievement is null)
            {
                container.Achievements.Add(new Achievement
                {
                    AchievementId = achievementId,
                    Unlocked = true,
                    New = true
                });
            }
            else
            {
                achievement.Unlocked = true;
                achievement.New = true;
            }
        }
        
        //update limited achievements
        foreach (int limitedAchievementId in achievementData.LimitedAchievements)
        {
            LimitedAchievement? limitedAchievement = container.LimitedAchievements.FirstOrDefault(a => a.LimitedAchievementId == limitedAchievementId);
            if (limitedAchievement is null)
            {
                container.LimitedAchievements.Add(new LimitedAchievement()
                {
                    LimitedAchievementId = limitedAchievementId,
                    Unlocked = true,
                    New = true
                });
            }
            else
            {
                limitedAchievement.Unlocked = true;
                limitedAchievement.New = true;
            }
        }
        
        //update achievement record books
        //todo: figure out if the game returns the full list of record books everytime or just the deltas (+1 on elements)
        //todo: update: seems to be just the deltas
        //todo: update: needs more research, definitely seems to not be just deltas
        
        //version that replaces data:
        /*
        container.AchievementRecordBooks.Clear();
        
        foreach (AchievementRecordBook recordBook in achievementData.RecordBooks)
        {
            container.AchievementRecordBooks.Add(recordBook);
        }*/
        
        //version that updates data:
        foreach (AchievementRecordBook recordBook in achievementData.RecordBooks)
        {
            AchievementRecordBook? existingRecordBook = container.AchievementRecordBooks.FirstOrDefault(r => r.Type == recordBook.Type);
            if (existingRecordBook is null)
            {
                container.AchievementRecordBooks.Add(recordBook);
            }
            else
            {
                //update the values with the values in the request
                for(int i = 0; i < existingRecordBook.Values.Length; i++)
                {
                    existingRecordBook.Values[i] = recordBook.Values[i];
                }
            }
        }
        
        //save changes
        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new AchievementResponse
            {
                Achievements = container.Achievements.ToArray(),
                RecordBooks = container.AchievementRecordBooks.ToArray(),
                Honors = container.Honors.ToArray(),
                Stages = container.Stages.ToArray(),
                Items = container.Items.ToArray(),
            }
        };
    }
}
        
