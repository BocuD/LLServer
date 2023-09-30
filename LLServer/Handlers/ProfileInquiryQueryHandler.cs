using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;
using ProfileCard = LLServer.Database.Models.ProfileCard;

namespace LLServer.Handlers;

public record ProfileInquiryQuery(RequestBase request) : BaseRequest(request);

public class ProfileInquiryQueryHandler : ParamHandler<ProfileInquiryParam, ProfileInquiryQuery>
{
    public ProfileInquiryQueryHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<ProfileInquiryParam, ProfileInquiryQuery>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
        
    }

    protected override async Task<ResponseContainer> HandleRequest(ProfileInquiryParam param, CancellationToken cancellationToken)
    {
        ProfileCard? card = await dbContext.ProfileCards
            .FirstOrDefaultAsync(c => c.ProfileCardId == param.ProfileCardId, cancellationToken);

        if (card == null)
        {
            logger.LogWarning("Profile card not found");
            return StaticResponses.EmptyResponse;
        }
        
        User? owner = await dbContext.Users
            .Where(u => u.UserId == card.UserID)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.GameHistory)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (owner == null)
        {
            logger.LogWarning("Profile card owner not found");
            return StaticResponses.EmptyResponse;
        }
        
        GameHistory? history = owner.GameHistory.FirstOrDefault(g => g.DbId == card.GameHistoryId);
        
        if (history == null)
        {
            logger.LogWarning("Profile card game history not found");
            return StaticResponses.EmptyResponse;
        }

        GameInformation gameInformation = new GameInformationMapper().FromGameHistoryBase(history);
        gameInformation.GameVersion = 65535;
        gameInformation.ProfileVersion = 65535;

        return new ResponseContainer
        {
            Result = 200,
            Response = new ProfileInquiryResponse
            {
                OwnerId = owner.UserId,
                FirstRelation = 0,
                UserData = owner.UserData,
                UserDataAqours = owner.UserDataAqours,
                UserDataSaintSnow = owner.UserDataSaintSnow,
                GameInformation = gameInformation
            }
        };
    }
}