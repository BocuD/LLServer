using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Session;
using MediatR;

namespace LLServer.Handlers;

public abstract record BaseRequest<RequestType>(RequestType request) : IRequest<ResponseContainer> where RequestType : RequestBase;

public abstract class BaseHandler<RequestType, ParamType> : IRequestHandler<BaseRequest<RequestType>, ResponseContainer> where RequestType : RequestBase
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<BaseHandler<RequestType, ParamType>> logger;
    private readonly SessionHandler sessionHandler;

    protected BaseHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<RequestType, ParamType>> logger, SessionHandler sessionHandler)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.sessionHandler = sessionHandler;
    }
    
    public async Task<ResponseContainer> Handle(BaseRequest<RequestType> request, CancellationToken cancellationToken)
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
        
        return HandleRequest(param, cancellationToken);
    }

    protected abstract ResponseContainer HandleRequest(ParamType param, CancellationToken cancellationToken);
}