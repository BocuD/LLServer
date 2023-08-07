using System.Text.Json;
using LLServer.Models.Information;
using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers.Information;

public record InformationQuery(string BaseUrl) : IRequest<ResponseContainer>;

public class InformationQueryHandler : IRequestHandler<InformationQuery, ResponseContainer>
{
    private readonly ILogger<InformationQueryHandler> logger;

    public InformationQueryHandler(ILogger<InformationQueryHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(InformationQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{request.BaseUrl}/info/",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
                MusicInformationItems = new List<MusicInformation>(),
                /*ResourceInformationItems = new List<ResourceInformation>()
                {
                    new ResourceInformation()
                    {
                        Category = 0,
                        Enable = true,
                        Hash = "0",
                        Id = 69420,
                        Image = "shitpost.jpg",
                        ResourceId = "69420",
                        Title = "Test",
                    }
                },
                InformationItems = new List<Models.Information.Information>()
                {
                    new()
                    {
                        Category = 0,
                        DisplayCenter = 1,
                        DisplaySatellite = true,
                        Enable = true,
                        StartDatetime = (DateTime.Now - TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
                        EndDatetime = (DateTime.Now + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
                        Id = 69420,
                        Image = "shitpost.jpg",
                        Order = 0,
                        Resource = "69420",
                        Title = "Test"
                    }
                }*/
            }
        };
        logger.LogInformation("Information: {Info}", JsonSerializer.Serialize(response));
        return response;
    }
}