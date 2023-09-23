using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Session;
using MediatR;

namespace LLServer.Handlers;

public class EmptyParam
{
}

public abstract record BaseRequest(RequestBase request) : IRequest<ResponseContainer>;

public abstract class ParamHandler<ParamType, RequestType> : IRequestHandler<RequestType, ResponseContainer> where RequestType : BaseRequest
{
    protected readonly ApplicationDbContext dbContext;
    protected readonly ILogger<ParamHandler<ParamType, RequestType>> logger;
    protected readonly SessionHandler sessionHandler;

    protected GameSession session;
    
    public ParamHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<ParamType, RequestType>> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }

    public virtual async Task<ResponseContainer> Handle(RequestType request, CancellationToken cancellationToken)
    {
        if (request.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get session
        GameSession? session_ = await sessionHandler.GetSession(request.request, cancellationToken);

        if (session_ is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        session = session_;

        //deserialize param
        string paramJson = request.request.Param.Value.GetRawText();

        ParamType? param = JsonSerializer.Deserialize<ParamType>(paramJson);

        if (param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        return await HandleRequest(param, cancellationToken);
    }

    protected virtual async Task<ResponseContainer> HandleRequest(ParamType param, CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}