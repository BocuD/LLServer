using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Session;
using MediatR;

namespace LLServer.Handlers;

public abstract class BaseHandler<RequestType> : IRequestHandler<RequestType, ResponseContainer> where RequestType : BaseRequest
{
    protected readonly ApplicationDbContext dbContext;
    protected readonly ILogger<BaseHandler<RequestType>> logger;
    protected readonly SessionHandler sessionHandler;

    protected GameSession session;
    
    public BaseHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<RequestType>> logger, SessionHandler sessionHandler)
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
        
        return await HandleRequest(cancellationToken);
    }

    protected virtual async Task<ResponseContainer> HandleRequest(CancellationToken cancellationToken)
    {
        return StaticResponses.EmptyResponse;
    }
}