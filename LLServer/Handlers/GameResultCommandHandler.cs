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
        var session = await dbContext.Sessions
            .AsSplitQuery()
            .Where(s => s.SessionId == command.request.SessionKey)
            .Select(s => new
            {
                Session = s,
                User = s.User,
                UserData = s.User.UserData,
                UserDataAqours = s.User.UserDataAqours,
                UserDataSaintSnow = s.User.UserDataSaintSnow,
                Members = s.User.Members,
                LiveDatas = s.User.LiveDatas,
            }).FirstOrDefaultAsync(cancellationToken);
        
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
        container.UserData.TotalExp = gameResult.TotalExp;
        container.UserData.Level = gameResult.DUserLevel;
        
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
                //check if we already have the chart unlocked (so if it exists in the cache)
                PersistentLiveData? data = container.PersistentLives.FirstOrDefault(x => x.LiveId == id);
                if (data == null)
                {
                    //add it to the persistent data
                    container.PersistentLives.Add(new PersistentLiveData
                    {
                        LiveId = id,
                        Unlocked = true,
                        New = true
                    });
                }
            }
        }

        //add score to user data
        PersistentLiveData? liveData = container.PersistentLives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
        
        if (liveData == null)
        {
            container.PersistentLives.Add(new PersistentLiveData
                {
                    LiveId = gameResult.LiveId,
                    Unlocked = true,
                    New = false
                });

            liveData = container.PersistentLives.FirstOrDefault(x => x.LiveId == gameResult.LiveId);
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
            
            if (gameResult.FinalePoint > 0)
            {
                liveData.FinaleCount++;
            }

            liveData.SelectCount++;
            liveData.PlayerCount1++;
        }
        
        //record game history
        //todo: make game history actually persistent
        List<GameHistoryBase> gameHistory;
        switch (gameResult.IdolKind)
        {
            case 0:
            default:
                gameHistory = PersistentUserDataContainer.GameHistory;
                break;
            case 1:
                gameHistory = PersistentUserDataContainer.GameHistoryAqours;
                break;
            case 2:
                gameHistory = PersistentUserDataContainer.GameHistorySaintSnow;
                break;
        }

        //todo: figure out if the game expects incrementing ids per type or of all histories together
        ulong highestId = gameHistory.Count == 0 ? 0 : gameHistory.Max(x => x.Id);

        GameHistoryBase newHistory = new()
        {
            Id = highestId + 1,
            PlayPlace = "test",
            Created = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"),
            DUserId = session.User.UserId,
            CharacterId = gameResult.CharacterId,
            MemberCardId = gameResult.MembercardId,
            UsedMemberCard = gameResult.UsedMemberCard,
            YellRank = gameResult.YellRank,
            Badge = gameResult.Badge,
            Nameplate = gameResult.Nameplate,
            Honor = gameResult.Honor,
            SkillCardsMain = gameResult.SkillCardsMain,
            SkillCardsCamera = gameResult.SkillCardsCamera,
            SkillCardsStage = gameResult.SkillCardsStage,
            SkillLevelsMain = gameResult.SkillLevelsMain,
            SkillLevelsCamera = gameResult.SkillLevelsCamera,
            SkillLevelsStage = gameResult.SkillLevelsStage,
            SkillStatusMain = gameResult.SkillStatusMain,
            SkillStatusCamera = gameResult.SkillStatusCamera,
            SkillStatusStage = gameResult.SkillStatusStage,
            LiveId = gameResult.LiveId,
            StageId = gameResult.StageId,
            EventMode = gameResult.EventMode,
            MemberCount = gameResult.MemberCount,
            PlayPart = gameResult.PlayPart,
            MaxCombo = gameResult.MaxCombo,
            FullCombo = gameResult.FullCombo,
            NoteMissCount = gameResult.NoteMissCount,
            NoteBadCount = gameResult.NoteBadCount,
            NoteGoodCount = gameResult.NoteGoodCount,
            NoteGreatCount = gameResult.NoteGreatCount,
            NotePerfectCount = gameResult.NotePerfectCount,
            FinalePoint = gameResult.FinalePoint,
            TechnicalScore = gameResult.TechnicalScore,
            SkillScore = gameResult.SkillScore,
            SynchroScore = gameResult.SynchroScore,
            ComboScore = gameResult.ComboScore,
            TechnicalRank = gameResult.TechnicalRank
        };
        //add to history
        gameHistory.Add(newHistory);
        
        //remove the first history if we have more than 10
        if (gameHistory.Count > 10)
        {
            gameHistory.RemoveAt(0);
        }
        
        //write to db
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new GameResultResponse()
            {
                Musics = container.Musics,
                Stages = container.Stages,
                EventResult = new EventResult(),
                EventRewards = new List<EventReward>(),
                EventStatus = new EventStatus()
            }
        };
    }
}