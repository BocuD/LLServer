﻿using LLServer.Database;
using LLServer.Mappers;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameEntryQuery(RequestBase request) : BaseRequest(request);

public class GameEntryQueryHandler : ParamHandler<GameEntryParam, GameEntryQuery>
{
    public GameEntryQueryHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<GameEntryParam, GameEntryQuery>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameEntryParam param, GameEntryQuery request,
        CancellationToken cancellationToken)
    {
        //todo: this seems to not be working
        // Mark the session as active and set the expire time
        session.IsActive = true;
        session.ExpireTime = DateTime.UtcNow.AddMinutes(60);
        
        if (session.IsGuest)
        {
            return await SatelliteGuestGameEntryResponse(param, cancellationToken);
        }
        else
        {
            return await SatelliteGameEntryResponse(param, request, cancellationToken);
        }
    }
    
    private async Task<ResponseContainer> SatelliteGuestGameEntryResponse(GameEntryParam gameEntryParam,
        CancellationToken cancellationToken)
    {
        PersistentUserDataContainer container = new(dbContext, session);

        //response
        GameEntryResponseMapper mapper = new();
        GameEntryResponse response = mapper.FromPersistentUserData(container);

        //copy over userdata and userdataaqours manually so we can safely modify them
        response.UserData = new UserData();
        response.UserDataAqours = new UserDataAqours();
        response.UserData = ReflectionMapper.Map(container.UserData, response.UserData);
        response.UserDataAqours = ReflectionMapper.Map(container.UserDataAqours, response.UserDataAqours);

        response.UserData.CharacterId = 1;
        response.UserDataAqours.CharacterId = 11;

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }

    private async Task<ResponseContainer> SatelliteGameEntryResponse(GameEntryParam gameEntryParam,
        GameEntryQuery request,
        CancellationToken cancellationToken)
    {
        //load user from db
        session.User = await dbContext.Users
            .Where(u => u.UserId == session.UserId)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.UserDataAqours)
            .Include(u => u.UserDataSaintSnow)
            .Include(u => u.Members)
            .Include(u => u.MemberCards)
            .Include(u => u.SkillCards)
            .Include(u => u.MemorialCards)
            .Include(u => u.TravelPamphlets)
            .Include(u => u.TravelTalks)
            .Include(u => u.Items)
            .Include(u => u.SpecialItems)
            .Include(u => u.MailBox)
            .FirstOrDefaultAsync(cancellationToken);

        PersistentUserDataContainer container = new(dbContext, session);

        container.UserData.PlaySatellite++;

        string lastPlayDate = container.UserData.PlayDate;
        
        //update last play time (format is "2023-07-02 00:00:00")
        container.UserData.PlayDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //unlock released pamphlets
        foreach (int releasedId in gameEntryParam.ReleasePamphletIds)
        {
            //add the pamphlet as new if it doesn't exist
            TravelPamphlet? pamphlet = container.TravelPamphlets.FirstOrDefault(p => p.TravelPamphletId == releasedId);

            if (pamphlet == null)
            {
                container.TravelPamphlets.Add(new TravelPamphlet
                {
                    TravelPamphletId = releasedId,
                    IsNew = true,
                    Round = 0
                });
            }
        }
        
        
        
        
        //temp: add memorial cards that are missing
        container.MemorialCards.AddRange(MemberCardData.InitialMemorialCards
            .Where(x => x < 10000)
            .Where(x => x != 0 && container.MemorialCards.All(y => y.CardMemorialId != x)).Select(x =>
                new MemorialCardData
                {
                    CardMemorialId = x,
                    Count = 1,
                    New = false
                }));
        
        
        
        
        await container.SaveChanges(cancellationToken);

        //generate response
        GameEntryResponseMapper mapper = new();
        GameEntryResponse response = mapper.FromPersistentUserData(container);
        
        response.UserData.PlayDate = lastPlayDate;

        return new ResponseContainer
        {
            Result = 200,
            Response = response
        };
    }
}
