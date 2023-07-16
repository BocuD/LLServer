using LLServer.Common;
using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record CheckWordCommand : IRequest<ResponseContainer>;

public class CheckWordCommandHandler : IRequestHandler<CheckWordCommand, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(CheckWordCommand request, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}