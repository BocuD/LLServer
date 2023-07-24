using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record AchievementCommand(RequestBase request) : IRequest<ResponseContainer>;

public class AchievementCommandHandler : IRequestHandler<AchievementCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AchievementCommandHandler> logger;

    public AchievementCommandHandler(ApplicationDbContext dbContext, ILogger<AchievementCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(AchievementCommand command, CancellationToken cancellationToken)
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
                AchievementRecordBooks = s.User.AchievementRecordBooks
            }).FirstOrDefaultAsync(cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        AchievementParam? achievementData = JsonSerializer.Deserialize<AchievementParam>(paramJson);
        if (achievementData is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        //update achievement record books
        //todo: figure out if the game returns the full list of record books everytime or just the deltas (+1 on elements)
        container.AchievementRecordBooks.Clear();
        
        foreach (AchievementRecordBook recordBook in achievementData.RecordBooks)
        {
            container.AchievementRecordBooks.Add(recordBook);
        }
        
        //save changes
        await dbContext.SaveChangesAsync(cancellationToken);

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
        