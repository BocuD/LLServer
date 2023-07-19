using System.Text.Json;
using LLServer.Common;
using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameResultCommand(RequestBase request) : IRequest<ResponseContainer>;

public class GameResultCommandHandler : IRequestHandler<GameResultCommand, ResponseContainer>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<GameResultCommandHandler> logger;

    public GameResultCommandHandler(ApplicationDbContext dbContext, ILogger<GameResultCommandHandler> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }
    
    public async Task<ResponseContainer> Handle(GameResultCommand command, CancellationToken cancellationToken)
    {
        if (command.request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get session
        Session? session = await dbContext.Sessions
            .Include(s => s.User)
            .Include(s => s.User.UserData)
            .Include(s => s.User.UserDataAqours)
            .Include(s => s.User.UserDataSaintSnow)
            .Include(s => s.User.Members)
            .FirstOrDefaultAsync(s => 
                    s.SessionId == command.request.SessionKey, 
                cancellationToken);
        
        if (session is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        string paramJson = command.request.Param.Value.GetRawText();

        //get game result
        GameResult? gameResult = JsonSerializer.Deserialize<GameResult>(paramJson);
        if (gameResult is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session.User);
        
        //update profile data
        container.UserData.Honor += gameResult.Honor;
        container.UserData.TotalExp += gameResult.GotExp;
        
        //update member usage count
        MemberData? memberData = container.Members.FirstOrDefault(x => x.CharacterId == gameResult.CharacterId);
        if (memberData == null)
        {
            container.Members.Add(new MemberData()
            {
                CharacterId = gameResult.CharacterId,
                CardMemberId = gameResult.UsedMemberCard
            });
            memberData = container.Members.FirstOrDefault(x => x.CharacterId == gameResult.CharacterId);
        }

        if (memberData != null)
        {
            memberData.SelectCount++;
        }

        //unlock lives
        if (gameResult.UnlockLiveIdArray.Length > 0)
        {
            foreach (int id in gameResult.UnlockLiveIdArray)
            {
                LiveData? data = container.Lives.FirstOrDefault(x => x.LiveId == id);
                if (data == null)
                {
                    container.Lives.Add(new LiveData() { LiveId = id });
                    data = container.Lives.FirstOrDefault(x => x.LiveId == id);
                }

                if (data != null)
                {
                    data.Unlocked = true;
                    data.New = true;
                }
            }
        }

        //add score to user data
        LiveData? liveData = container.Lives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
        if (liveData == null)
        {
            container.Lives.Add(new LiveData()
            {
                LiveId = gameResult.LiveId,
                Unlocked = true
            });

            liveData = container.Lives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
        }
        
        if (liveData != null)
        {
            if (gameResult.TotalScore > liveData.TotalHiScore)
            {
                liveData.TotalHiScore = gameResult.TotalScore;
            }

            if (gameResult.TechnicalRate > liveData.TechnicalHiRate)
            {
                liveData.TechnicalHiRate = gameResult.TechnicalRate;
            }

            if (gameResult.TechnicalScore > liveData.TechnicalHiScore)
            {
                liveData.TechnicalHiScore = gameResult.TechnicalScore;
            }

            if (gameResult.TechnicalRank > liveData.TechnicalRank)
            {
                liveData.TechnicalRank = gameResult.TechnicalRank;
            }

            if (gameResult.FullCombo > 0)
            {
                liveData.FullCombo = gameResult.FullCombo;
            }

            liveData.SelectCount++;
            liveData.PlayerCount1++;
        }

        return new ResponseContainer
        {
            Result = 200,
            Response = new GameResultResponse()
            {
                Musics = MusicData.GetBaseMusicData(),
                Stages = StageData.GetBaseStageData(),
                EventResult = new EventResult(),
                EventRewards = new List<EventReward>(),
                EventStatus = new EventStatus()
            }
        };
    }
}