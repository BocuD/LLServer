using LLServer.Models;
using MediatR;

namespace LLServer.Handlers;

public record InformationQuery(string BaseUrl) : IRequest<ResponseContainer>;

public class InformationQueryHandler : IRequestHandler<InformationQuery, ResponseContainer>
{
    public static ResponseContainer Handle(string serveripaddress)
    {
        return new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{serveripaddress}/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd")
            }
        };
    }

    public async Task<ResponseContainer> Handle(InformationQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer
        {
            Result = 200,
            Response = new InformationResponse
            {
                BaseUrl = $"http://{request.BaseUrl}/game",
                EncoreExpirationDate = (DateTime.Today + TimeSpan.FromDays(3650)).ToString("yyyy-MM-dd")
            }
        };
        return response;
    }
}