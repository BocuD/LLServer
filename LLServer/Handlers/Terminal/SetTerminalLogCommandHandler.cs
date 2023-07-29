using LLServer.Common;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using MediatR;

namespace LLServer.Handlers.Terminal;

public record SetTerminalLogCommand(RequestBase request) : IRequest<ResponseContainer>;

public class SetTerminalLogCommandHandler : IRequestHandler<SetTerminalLogCommand, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(SetTerminalLogCommand request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}