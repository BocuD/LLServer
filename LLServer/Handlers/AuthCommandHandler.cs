using LLServer.Models;
using MediatR;

namespace LLServer.Handlers;

public record AuthCommand() : IRequest<ResponseContainer>;

public class AuthCommandHandler : IRequestHandler<AuthCommand, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer
        {
            Result = 200,
            Response = new AuthResponse
            {
                AbnormalEnd = 0,
                BlockSequence = 1,
                Name = "",
                SessionKey = "12345678901234567890123456789012",
                Status = 0,
                UserId = "1"
            }
        };
        return response;
    }
}