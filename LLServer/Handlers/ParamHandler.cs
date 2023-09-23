using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Models.Responses;
using LLServer.Session;

namespace LLServer.Handlers;

public abstract class ParamHandler<ParamType, RequestType> : BaseHandler<RequestType> where RequestType : BaseRequest
{
    protected ParamHandler(ApplicationDbContext dbContext, ILogger<BaseHandler<RequestType>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }
    
    public override async Task<ResponseContainer> Handle(RequestType request, CancellationToken cancellationToken)
    {
        //todo: this will still call the base handler's HandleRequest() method, which is a little cursed but it works for now
        await base.Handle(request, cancellationToken);
        
        //deserialize from param
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