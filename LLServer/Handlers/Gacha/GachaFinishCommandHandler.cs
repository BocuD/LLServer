using LLServer.Common;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using MediatR;

namespace LLServer.Handlers.Gacha;

public record GachaFinishCommmand(RequestBase request) : IRequest<ResponseContainer>;

public class GachaFinishCommandHandler : IRequestHandler<GachaFinishCommmand, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(GachaFinishCommmand request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}