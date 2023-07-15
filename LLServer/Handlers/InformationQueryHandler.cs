using LLServer.Models;
using LLServer.Models.Information;
using MediatR;

namespace LLServer.Handlers;

public record InformationQuery(string BaseUrl) : IRequest<ResponseContainer>;

public class InformationQueryHandler : IRequestHandler<InformationQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(InformationQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{request.BaseUrl}/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd"),
                MusicInformationItems = Enumerable.Range(1,20)
                    .Select(i => new MusicInformation
                    {
                        MusicId = i
                    }).ToList()
            }
        };
        return response;
    }
}