using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Requests.Gacha;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Responses;
using LLServer.Models.Responses.Gacha;
using LLServer.Session;
using MediatR;

namespace LLServer.Handlers.Gacha;

public record MemberGachaQuery(RequestBase request) : IRequest<ResponseContainer>;

public class MemberGachaQueryHandler : IRequestHandler<MemberGachaQuery, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<MemberGachaQueryHandler> logger;
    private readonly SessionHandler sessionHandler;

    public MemberGachaQueryHandler(ApplicationDbContext dbContext, ILogger<MemberGachaQueryHandler> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }
    
    public async Task<ResponseContainer> Handle(MemberGachaQuery query, CancellationToken cancellationToken)
    {
        if (query.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //todo: load user data or gacha shit from a database idk lol
        // GameSession? session = await sessionHandler.GetSession(command.request, cancellationToken);
        //
        // if (session is null)
        // {
        //     return StaticResponses.BadRequestResponse;
        // }
        //
        // if (!session.IsGuest)
        // {
        //     session.User = await dbContext.Users
        //         .Where(u => u.UserId == session.UserId)
        //         .AsSplitQuery()
        //         .Include(u => u.UserData)
        //         .Include(u => u.UserDataAqours)
        //         .Include(u => u.UserDataSaintSnow)
        //         .Include(u => u.Members)
        //         .Include(u => u.MemberCards)
        //         .Include(u => u.LiveDatas)
        //         .Include(u => u.TravelData)
        //         .Include(u => u.TravelPamphlets)
        //         .Include(u => u.TravelHistory)
        //         .Include(u => u.TravelHistoryAqours)
        //         .Include(u => u.TravelHistorySaintSnow)
        //         .Include(u => u.Items)
        //         .Include(u => u.SpecialItems)
        //         .FirstOrDefaultAsync(cancellationToken);
        // }
        // else
        // {
        //     return new ResponseContainer
        //     {
        //         Result = 200,
        //         Response = new TravelResultResponse()
        //     };
        // }

        string paramJson = query.request.Param.Value.GetRawText();

        //get game result
        MemberGachaParam? gachaRequest = JsonSerializer.Deserialize<MemberGachaParam>(paramJson);
        if (gachaRequest is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        return new ResponseContainer
        {
            Result = 200,
            Response = new MemberGachaResponse
            {
                //todo: un hardcode this thing
                GachaId = int.Parse(gachaRequest.GachaId),
                LastCrackers = Array.Empty<int>(),
                Box = new[]
                {
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    },
                    new GachaBox
                    {
                        Category = 1,
                        CharacterId = 7,
                        ItemId = 70041,
                        Level = 1,
                        Enable = 1
                    }
                }
            }
        };
    }
}