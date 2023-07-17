﻿using System.Text.Json;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record GetUserDataQuery : IRequest<ResponseContainer>;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, ResponseContainer>
{
    private readonly ILogger<GetUserDataQueryHandler> logger;

    public GetUserDataQueryHandler(ILogger<GetUserDataQueryHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<ResponseContainer> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();
        var response = new ResponseContainer()
        {
            Result = 200,
            Response = userDataContainer.GetUserData()
        };

        return response;
    }
}