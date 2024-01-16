using LLServer.Models.Responses;

namespace LLServer.Common;

public static class StaticResponses
{
    public static readonly ResponseContainer EmptyResponse = new()
    {
        Result = StatusCodes.Status200OK,
        Response = new ResponseBase()
    };

    public static readonly ResponseContainer BadRequestResponse = new()
    {
        Result = StatusCodes.Status400BadRequest,
        Response = new ResponseBase()
    };
}
