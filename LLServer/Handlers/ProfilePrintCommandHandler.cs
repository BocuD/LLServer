using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record ProfilePrintCommand(RequestBase request) : BaseRequest(request);

public class ProfilePrintCommandHandler : ParamHandler<ProfilePrintParam, ProfilePrintCommand>
{
    public ProfilePrintCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<ProfilePrintParam, ProfilePrintCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(ProfilePrintParam param, ProfilePrintCommand request,
        CancellationToken cancellationToken)
    {
        session.User = await dbContext.Users
            .Where(u => u.UserId == session.UserId)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.GameHistory)
            .FirstOrDefaultAsync(cancellationToken);
        
        //create new profile card
        ProfileCard newCard = new()
        {
            //generate a new profile card id
            ProfileCardId = Guid.NewGuid().ToString().Replace("-", string.Empty),
            GameHistory = session.User.GameHistory.FirstOrDefault(g => g.Id == param.GameHistoryId),
            User = session.User
        };
        
        //add the new profile card to the database
        await dbContext.ProfileCards.AddAsync(newCard, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new ProfilePrintResponse
            {
                ProfileCardId = newCard.ProfileCardId,
                Created = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")
            }
        };
    }
}
