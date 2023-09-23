using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GetMemberCardQuery(RequestBase request) : BaseRequest(request);

public class GetMemberCardCommandHandler : BaseHandler<GetMemberCardParam, GetMemberCardQuery>
{
    public GetMemberCardCommandHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<GetMemberCardParam, GetMemberCardQuery>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameSession session, GetMemberCardParam getMemberCard, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Members)
                .Include(u => u.MemberCards)
                .FirstOrDefaultAsync(cancellationToken);
        }

        //get persistent userdata container
        PersistentUserDataContainer container = new(dbContext, session);

        //add member card to database
        MemberCardData? existingCard =
            container.MemberCards.FirstOrDefault(c => c.CardMemberId == getMemberCard.MemberCardId);

        if (existingCard != null)
        {
            existingCard.Count++;
        }
        else
        {
            container.MemberCards.Add(new MemberCardData
            {
                CardMemberId = getMemberCard.MemberCardId,
                Count = 1,
                New = true,
                PrintRest = 1
            });
        }

        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new GetMemberCardResponse
            {
                MemberCard = container.MemberCards.ToArray()
            }
        };
    }
}