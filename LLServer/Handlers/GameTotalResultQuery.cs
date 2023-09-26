using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameTotalResultQuery(RequestBase request) : BaseRequest(request);

public class GameTotalResultQueryHandler : ParamHandler<EmptyParam, GameTotalResultQuery>
{
    public GameTotalResultQueryHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<EmptyParam, GameTotalResultQuery>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    protected override async Task<ResponseContainer> HandleRequest(EmptyParam param, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.Items)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        return new ResponseContainer
        {
            Result = 200,
            Response = new GameTotalResultResponse
            {
                Items = container.Items.ToArray(),
                Cheer = new CheerInfo
                {
                    YellCount = 0,
                    CheerRank = 1,
                    RewardMobilePoint = 1,
                    Now = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"),
                    CheerPlayers = new List<CheerPlayer>
                    {
                        new()
                        {
                            StampId = 1,
                            YellRank = 1,
                            CharacterId = 1,
                            Name = "ゲストプレイヤー",
                            Nameplate = 1,
                            Badge = 1
                        }
                    }
                }
            }
        };
    }
}