using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record AchievementYellCommand(RequestBase request) : IRequest<ResponseContainer>;

public class AchievementYellCommandHandler : IRequestHandler<AchievementYellCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AchievementYellCommandHandler> logger;

    public AchievementYellCommandHandler(ApplicationDbContext dbContext, ILogger<AchievementYellCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(AchievementYellCommand command, CancellationToken cancellationToken)
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
                TravelPamphlets = s.User.TravelPamphlets,
                TravelHistory = s.User.TravelHistory,
                TravelHistoryAqours = s.User.TravelHistoryAqours,
                TravelHistorySaintSnow = s.User.TravelHistorySaintSnow,
                YellAchievements = s.User.YellAchievements,
                AchievementRecordBooks = s.User.AchievementRecordBooks,
                Items = s.User.Items,
                SpecialItems = s.User.SpecialItems
            }).FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        AchievementYellParam? achievementData = JsonSerializer.Deserialize<AchievementYellParam>(paramJson);
        if (achievementData is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //todo: record mobile points (found in smallrewardcount)

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session.User);

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
        
        //todo: figure out if all yell achievements are in the request or just the new ones
        //apply yell achievements
        //if all yell achievements are in the request, clear the old ones first:
        //container.YellAchievements.Clear();
        container.YellAchievements.AddRange(achievementData.YellAchievements);
        
        //save changes
        await dbContext.SaveChangesAsync(cancellationToken);

        //todo: return actual data
        return new ResponseContainer()
        {
            Result = 200,
            Response = new AchievementYellResponse
            {
                YellAchievements = container.YellAchievements.ToArray(),
                Honors = Array.Empty<HonorData>(),
                Items = container.Items.ToArray(),
                MemberCards = container.MemberCards.ToArray(),
                Stages = container.Stages.ToArray()
            }
        };
    }
}