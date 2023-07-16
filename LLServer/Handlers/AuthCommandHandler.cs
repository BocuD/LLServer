using LLServer.Models;
using LLServer.Models.Responses;
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
                Name = "Test123456",
                SessionKey = "12345678901234567890123456789012",
                // 1 for new card?
                Status = 1,
                UserId = "1"
            }
        };
        return response;
    }
}