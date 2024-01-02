using LLServer.Event;
using LLServer.Models.Information;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers.Information;

public record InformationQuery(RequestBase request, string baseUrl) : BaseRequest(request);

public class InformationQueryHandler : IRequestHandler<InformationQuery, ResponseContainer>
{
    private readonly ILogger<InformationQueryHandler> logger;
    private readonly EventDataProvider eventDataProvider;

    public InformationQueryHandler(ILogger<InformationQueryHandler> logger, EventDataProvider eventDataProvider)
    {
        this.logger = logger;
        this.eventDataProvider = eventDataProvider;
    }

    public async Task<ResponseContainer> Handle(InformationQuery request, CancellationToken cancellationToken)
    {
        bool isTerminal = request.request.Terminal.TerminalAttrib == 1;
        
        var response = new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{request.baseUrl}/info/",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
                MusicInformationItems = new List<MusicInformation>(),
                ResourceInformationItems = eventDataProvider.GetResourceInformation(),
                InformationItems = eventDataProvider.GetInformation(isTerminal),
                GachaInformation = new GachaInformation
                {
                    GachaList = new List<GachaInformationItem>
                    {
                        new()
                        {
                            Id = 1,
                            IdolKind = 0,
                            GachaType = 1, //birthday
                            CharacterId = 1,
                            Priority = 1,
                            DrawLimit = 0,
                            CrackerEnable = 1,
                            IsEvent = 0,
                            FirstOnly = 0,
                            Rates = {1, 2, 3},
                            Resource = "456",
                            GachaId = "1",
                            Image = "info_503_1.jpg",
                            ImageEvent = "info_503_1.jpg",
                            Title = "test title",
                            Caption = "2",
                            MenuCaption = "3",
                            StartDatetime = "2021-01-0112:00:00",
                            EndDatetime = "2099-01-0112:00:00",
                            StartMenuDatetime = "2021-01-0112:00:00",
                            EndMenuDatetime = "2099-01-0112:00:00"
                        },
                        new()
                        {
                            Id = 2,
                            IdolKind = 0,
                            GachaType = 2,
                            CharacterId = 1,
                            Priority = 1,
                            DrawLimit = 0,
                            CrackerEnable = 1,
                            IsEvent = 0,
                            FirstOnly = 0,
                            Rates = {1, 2, 3},
                            Resource = "123",
                            GachaId = "1",
                            Image = "info_770_1.jpg",
                            ImageEvent = "info_770_1.jpg",
                            Title = "test title",
                            Caption = "2",
                            MenuCaption = "3",
                            StartDatetime = "2021-01-0112:00:00",
                            EndDatetime = "2099-01-0112:00:00",
                            StartMenuDatetime = "2021-01-0112:00:00",
                            EndMenuDatetime = "2099-01-0112:00:00"
                        },
                        new()
                        {
                            Id = 3,
                            IdolKind = 0,
                            GachaType = 3, //member gacha
                            CharacterId = 1,
                            Priority = 1,
                            DrawLimit = 0,
                            CrackerEnable = 1,
                            IsEvent = 0,
                            FirstOnly = 0,
                            Rates = {1, 2, 3},
                            Resource = "123",
                            GachaId = "1",
                            Image = "info_440_2.jpg",
                            ImageEvent = "info_440_2.jpg",
                            Title = "test title",
                            Caption = "2",
                            MenuCaption = "3",
                            StartDatetime = "2021-01-0112:00:00",
                            EndDatetime = "2099-01-0112:00:00",
                            StartMenuDatetime = "2021-01-0112:00:00",
                            EndMenuDatetime = "2099-01-0112:00:00"
                        }
                    },
                    Notes = "wtf!!!"
                },
                EventInformationItems = new()
                {
                    // new()
                    // {
                    //     Id = 1,
                    //     Active = true,
                    //     EventType = 0,
                    //     PointType = 0,
                    //     CharacterId = 4,
                    //     PointMag = 2,
                    //     MemberTravelPamphletId = 50042,
                    //     StartDatetime = DateTime.Now - TimeSpan.FromDays(3650),
                    //     EndDatetime = DateTime.Now + TimeSpan.FromDays(3650),
                    //     ResBanner = "image2.jpg",
                    //     ResReward = "image2.jpg",
                    //     Title = "Test",
                    //     Musics = new List<int> { 10, },
                    //     Levels = new List<int> { 0 },
                    //     EventRewards = new List<EventReward>
                    //     {
                    //         new() 
                    //         {
                    //             Order = 0,
                    //             Id = 0,
                    //             RequirePoint = 10,
                    //             RewardArg = 0,
                    //             RewardType = 0
                    //         }
                    //     }
                    // }
                },
                RankingInformation = new RankingInformation()
            }
        };
        return response;
    }
}