using System.Text.Json;
using LLServer.Common;
using LLServer.Models;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using MediatR;

namespace LLServer.Handlers;

public record GameResultCommand(JsonElement? Param) : IRequest<ResponseContainer>;

public class GameResultCommandHandler : IRequestHandler<GameResultCommand, ResponseContainer>
{
    private readonly ILogger<GameResultCommandHandler> logger;

    public GameResultCommandHandler(ILogger<GameResultCommandHandler> logger)
    {
        this.logger = logger;
    }
    
    public async Task<ResponseContainer> Handle(GameResultCommand request, CancellationToken cancellationToken)
    {
        if (request.Param is null)
        {
            return StaticResponses.BadRequestResponse;
        }
        
        var paramJson = request.Param.Value.GetRawText();
        logger.LogInformation("ParamJson {ParamJson}", paramJson);

        var gameResult = JsonSerializer.Deserialize<GameResult>(paramJson);

        // Null is only possible when json string is "null"
        if (gameResult is null)
        {
            return StaticResponses.BadRequestResponse;
        }

        var userDataContainer = UserDataContainer.GetDummyUserDataContainer();

        //add score to user data
        var liveData = userDataContainer.Lives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
        if (liveData == null)
        {
            userDataContainer.Lives.Add(new LiveData()
            {
                LiveId = gameResult.LiveId,
                Unlocked = true
            });

            liveData = userDataContainer.Lives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
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

        //update profile data
        userDataContainer.UserData.Honor += gameResult.Honor;
        userDataContainer.UserData.TotalExp += gameResult.GotExp;

        //update member usage count
        var memberData =
            userDataContainer.Members.FirstOrDefault(x => x.CharacterId == gameResult.CharacterId);
        if (memberData == null)
        {
            userDataContainer.Members.Add(new MemberData()
            {
                CharacterId = gameResult.CharacterId,
                CardMemberId = gameResult.UsedMemberCard
            });
            memberData = userDataContainer.Members.FirstOrDefault(x => x.CharacterId == gameResult.CharacterId);
        }

        if (memberData != null)
        {
            memberData.SelectCount++;
        }

        //unlock lives
        if (gameResult.UnlockLiveIdArray.Length > 0)
        {
            foreach (var id in gameResult.UnlockLiveIdArray)
            {
                var data = userDataContainer.Lives.FirstOrDefault(x => x.LiveId == id);
                if (data == null)
                {
                    userDataContainer.Lives.Add(new LiveData() { LiveId = id });
                    data = userDataContainer.Lives.FirstOrDefault(x => x.LiveId == id);
                }

                if (data != null)
                    data.Unlocked = true;
            }
        }

        var response = new ResponseContainer
        {
            Result = 200,
            Response = new GameResultResponse()
            {
                Musics = userDataContainer.Musics,
                EventResult = new EventResult(),
                EventRewards = new List<EventReward>(),
                EventStatus = new EventStatus(),
                Stages = userDataContainer.Stages
            }
        };
        
        return response;
    }
}