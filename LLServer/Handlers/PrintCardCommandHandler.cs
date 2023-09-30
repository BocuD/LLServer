using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record PrintCardCommand(RequestBase request) : BaseRequest(request);

public class PrintCardCommandHandler : ParamHandler<PrintCardParam, PrintCardCommand>
{
    public PrintCardCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<PrintCardParam, PrintCardCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
        
    }

    protected override async Task<ResponseContainer> HandleRequest(PrintCardParam param, CancellationToken cancellationToken)
    {
        //load card data from db
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.MemberCards)
                .Include(u => u.SkillCards)
                .Include(u => u.MemorialCards)
                .FirstOrDefaultAsync(cancellationToken);
        }

        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, session);
        
        return new ResponseContainer
        {
            Result = 200,
            Response = new PrintCardResponse
            {
                MemberCards = container.MemberCards,
                SkillCards = container.SkillCards,
                MemorialCards = container.MemorialCards
            }
        };
    }
}