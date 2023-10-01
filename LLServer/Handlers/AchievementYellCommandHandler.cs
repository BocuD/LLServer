using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record AchievementYellCommand(RequestBase request) : BaseRequest(request);

public class AchievementYellCommandHandler : ParamHandler<AchievementYellParam, AchievementYellCommand>
{
    public AchievementYellCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<AchievementYellParam, AchievementYellCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(AchievementYellParam achievementData, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Members)
                .Include(u => u.YellAchievements)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }

        //todo: record mobile points (found in smallrewardcount)

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        //apply yell_achieve_rank
        foreach (MemberYellAchievement memberYellAchievement in achievementData.MemberYellAchievements)
        {
            //find a matching character id in members, if it doesn't exist add a new member entry
            MemberData? member = container.Members.FirstOrDefault(m => m.CharacterId == memberYellAchievement.CharacterId);
            if (member == null)
            {
                container.Members.Add(new MemberData
                {
                    CharacterId = memberYellAchievement.CharacterId,
                });
                member = container.Members.FirstOrDefault(m => m.CharacterId == memberYellAchievement.CharacterId);
            }

            if (member != null)
            {
                member.AchieveRank = memberYellAchievement.AchieveRank;
            }
        }
        
        //add new yell achievements
        foreach(int yellAchievement in achievementData.YellAchievements)
        {
            //find a matching yell achievement id in yell achievements, if it doesn't exist add a new yell achievement entry
            YellAchievement? yellAchievementEntry = container.YellAchievements.FirstOrDefault(ya => ya.YellAchievementId == yellAchievement);
            if (yellAchievementEntry == null)
            {
                container.YellAchievements.Add(new YellAchievement
                {
                    YellAchievementId = yellAchievement,
                    New = true,
                    Unlocked = true
                });
            }
        }

        //save changes
        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new AchievementYellResponse
            {
                YellAchievements = container.YellAchievements.ToArray(),
                Honors = container.Honors.ToArray(),
                Items = container.Items.ToArray(),
                MemberCards = container.MemberCards.ToArray(),
                Stages = container.Stages.ToArray()
            }
        };
    }
}