﻿using LLServer.Models.Responses;
using MediatR;

// ReSharper disable UnusedType.Global
namespace LLServer.Handlers;

public record GetRankingQuery : IRequest<ResponseContainer>;

public class GetRankingQueryHandler : IRequestHandler<GetRankingQuery, ResponseContainer>
{
    public async Task<ResponseContainer> Handle(GetRankingQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseContainer
        {
            Result = 200,
            Response = RankingResponse.DummyRankingResponse()
        };

        return response;
    }
}