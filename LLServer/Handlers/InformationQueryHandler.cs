using System.Text.Json;
using LLServer.Models.Information;
using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

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
                BaseUrl = $"http://{request.BaseUrl}/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
                MusicInformationItems = new List<MusicInformation>()
            }
        };
        logger.LogInformation("Information: {Info}", JsonSerializer.Serialize(response));
        return response;
    }
}