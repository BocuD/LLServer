using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record AchievementCommand(RequestBase request) : IRequest<ResponseContainer>;

public class AchievementCommandHandler : IRequestHandler<AchievementCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly SessionHandler sessionHandler;

    public AchievementCommandHandler(ApplicationDbContext dbContext, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.sessionHandler = sessionHandler;
    }

    public async Task<ResponseContainer> Handle(AchievementCommand command, CancellationToken cancellationToken)
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
                .Include(u => u.AchievementRecordBooks)
                .Include(u => u.Achievements)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return StaticResponses.EmptyResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get achievement data
        AchievementParam? achievementData = JsonSerializer.Deserialize<AchievementParam>(paramJson);
        if (achievementData is null)
        {
            return StaticResponses.BadRequestResponse;
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
        
        //todo: update limited achievements
        
        
        //update achievement record books
        //todo: figure out if the game returns the full list of record books everytime or just the deltas (+1 on elements)
        //todo: update: seems to be just the deltas
        
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
                //increment the values by the values in the request
                for(int i = 0; i < existingRecordBook.Values.Length; i++)
                {
                    existingRecordBook.Values[i] += recordBook.Values[i];
                }
            }
        }
        
        //save changes
        await container.SaveChanges(cancellationToken);

        //todo: return actual data
        return new ResponseContainer
        {
            Result = 200,
            Response = new AchievementResponse
            {
                Achievements = new Achievement[0],
                RecordBooks = container.AchievementRecordBooks.ToArray(),
                Honors = new HonorData[0],
                Stages = container.Stages.ToArray(),
                Items = new Item[0]
            }
        };
    }
}
        