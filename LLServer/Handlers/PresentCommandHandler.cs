using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record PresentCommand(RequestBase request) : BaseRequest(request);

public class PresentCommandHandler : ParamHandler<PresentParam, PresentCommand>
{
    public PresentCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<PresentParam, PresentCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(PresentParam param, CancellationToken cancellationToken)
    {
        session.User = dbContext.Users
            .Where(u => u.UserId == session.UserId)
            .AsSplitQuery()
            .Include(u => u.MemberCards)
            .Include(u => u.SkillCards)
            .Include(u => u.MemorialCards)
            .Include(u => u.Items)
            .Include(u => u.MailBox)
            .FirstOrDefault();
        
        PersistentUserDataContainer container = new(dbContext, session);

        string targetId = string.Empty;
        //check if the param.Id jsonelement is an array
        if (param.Id.ValueKind == JsonValueKind.Array)
        {
            //get the first element as string
            targetId = param.Id[0].GetString() ?? string.Empty;
        }
        else
        {
            targetId = param.Id.GetString() ?? string.Empty;
        }

        //find the mailbox item and remove it
        MailBoxItem? mailBoxItem = container.MailBox.FirstOrDefault(m => m.Id == targetId);

        if (mailBoxItem == null)
        {
            logger.LogWarning("Mailbox item {Id} not found for user {UserId}", targetId, session.UserId);
            return StaticResponses.EmptyResponse;
        }

        switch (mailBoxItem.Category)
        {
            case 1: //member card
                //add the card to the database
                MemberCardData? memberCardData =
                    container.MemberCards.FirstOrDefault(m => m.CardMemberId == mailBoxItem.ItemId);

                if (memberCardData == null)
                {
                    container.MemberCards.Add(new MemberCardData
                    {
                        CardMemberId = mailBoxItem.ItemId,
                        Count = 1,
                        New = true
                    });
                }
                else
                {
                    memberCardData.Count++;
                }
                break;
                
            case 2: //skill card
                if(param.Sell == 1)
                {
                    //todo give mobile points :P
                    break;
                }
                
                //add the card to the database
                SkillCardData? skillCardData = container.SkillCards.FirstOrDefault(s => s.CardSkillId == mailBoxItem.ItemId);

                if (skillCardData == null)
                {
                    container.SkillCards.Add(new SkillCardData
                    {
                        CardSkillId = mailBoxItem.ItemId,
                        SkillLevel = 1,
                        New = true
                    });
                }
                else
                {
                    skillCardData.SkillLevel++;
                }
                break;
                
            case 3: //seems to be memorial card
                //add the card to the database
                MemorialCardData? memorialCardData = container.MemorialCards.FirstOrDefault(m => m.CardMemorialId == mailBoxItem.ItemId);

                if (memorialCardData == null)
                {
                    container.MemorialCards.Add(new MemorialCardData
                    {
                        CardMemorialId = mailBoxItem.ItemId,
                        Count = 1,
                        New = true
                    });
                }
                else
                {
                    memorialCardData.Count++;
                }
                break;
                
            case 4: //seems to be item
                //todo test and maybe implement this
                break;
        }

        container.MailBox.Remove(mailBoxItem);

        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new PresentResponse
            {
                MemberCards = container.MemberCards.ToArray(),
                SkillCards = container.SkillCards.ToArray(),
                MemorialCards = container.MemorialCards.ToArray(),
                Items = container.Items.ToArray(),
                MailBox = container.MailBox.ToArray(),
            }
        };
    }
}