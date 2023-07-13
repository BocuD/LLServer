using LLServer.Models;

namespace LLServer.Common;

public static class StaticResponses
{
    public static ResponseContainer EmptyResponse = new ResponseContainer
    {
        Result = StatusCodes.Status200OK,
        Response = new ResponseBase()
    };

    public static ResponseContainer BadRequestResponse = new ResponseContainer
    {
        Result = StatusCodes.Status400BadRequest,
        Response = new ResponseBase()
    };
}