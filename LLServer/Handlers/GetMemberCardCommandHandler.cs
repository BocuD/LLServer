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

public record GetMemberCardQuery(RequestBase request) : IRequest<ResponseContainer>;

public class GetMemberCardCommandHandler : IRequestHandler<GetMemberCardQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GetUserDataQueryHandler> logger;
    private readonly SessionHandler sessionHandler;

    public async Task<ResponseContainer> Handle(GetMemberCardQuery command, CancellationToken cancellationToken)
    {
        GameSession? session = await sessionHandler.GetSession(command.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        GetMemberCardParam? getMemberCard = JsonSerializer.Deserialize<GetMemberCardParam>(paramJson);
        if (getMemberCard is null)
        {
            return StaticResponses.BadRequestResponse;
        }

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