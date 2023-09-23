using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Session;
using MediatR;

namespace LLServer.Handlers;

public abstract record BaseRequest(RequestBase request) : IRequest<ResponseContainer>;

public abstract class BaseHandler<ParamType, RequestType> : IRequestHandler<RequestType, ResponseContainer> where RequestType : BaseRequest
{
    protected readonly ApplicationDbContext dbContext;
    protected readonly ILogger<BaseHandler<ParamType, RequestType>> logger;
    protected readonly SessionHandler sessionHandler;

    public BaseHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<ParamType, RequestType>> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }
    
    public async Task<ResponseContainer> Handle(RequestType request, CancellationToken cancellationToken)
    {
        if (request.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //deserialize from param
        string paramJson = request.request.Param.Value.GetRawText();

        var param = JsonSerializer.Deserialize<ParamType>(paramJson);

        if (param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        //get session
        GameSession? session = await sessionHandler.GetSession(request.request, cancellationToken);

        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        return await HandleRequest(session, param, cancellationToken);
    }

    protected abstract Task<ResponseContainer> HandleRequest(GameSession session, ParamType param,
        CancellationToken cancellationToken);
}