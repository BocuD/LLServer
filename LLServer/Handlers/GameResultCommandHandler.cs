using LLServer.Database;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models;
using LLServer.Models.Requests;
using LLServer.Models.Responses;
using LLServer.Models.UserData;
using LLServer.Session;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Handlers;

public record GameResultCommand(RequestBase request) : BaseRequest(request);

public class GameResultCommandHandler : ParamHandler<GameResult, GameResultCommand>
{
    public GameResultCommandHandler(ApplicationDbContext dbContext, ILogger<ParamHandler<GameResult, GameResultCommand>> logger, SessionHandler sessionHandler) : base(dbContext, logger, sessionHandler)
    {
    }

    protected override async Task<ResponseContainer> HandleRequest(GameResult gameResult, CancellationToken cancellationToken)
    {
        if (!session.IsGuest)
        {
            session.User = await dbContext.Users
                .Where(u => u.UserId == session.UserId)
                .AsSplitQuery()
                .Include(u => u.UserData)
                .Include(u => u.UserDataAqours)
                .Include(u => u.UserDataSaintSnow)
                
                .Include(u => u.Members)
                .Include(u => u.LiveDatas)

                //todo only load relevant gamehistory data
                .Include(u => u.GameHistory)
                .Include(u => u.GameHistoryAqours)
                .Include(u => u.GameHistorySaintSnow)
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            return new ResponseContainer
            {
                Result = 200,
                Response = new GameResultResponse()
            };
        }
        
        //get persistent data container
        PersistentUserDataContainer container = new(dbContext, session);

        //update member usage count
        MemberData? memberData = container.Members.FirstOrDefault(x => x.CharacterId == gameResult.CharacterId);
        if (memberData == null)
        {
            container.Members.Add(new MemberData
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
            
            liveData.Unlocked = true;
            liveData.New = false;
        }
        
        //record game history
        ulong highestId = 1;
        switch (container.UserData.IdolKind)
        {
            //µ's
            case 0:
                if (container.GameHistory.Count == 0) break;
                
                highestId = container.GameHistory.Max(x => ulong.Parse(x.Id));
                
                //if we have more than 10 entries, remove the one with the lowest id
                if (container.GameHistory.Count > 10)
                {
                    container.GameHistory.Remove(container.GameHistory.First());
                }
                break;

            //aqours
            case 1:
                if (container.GameHistoryAqours.Count == 0) break;

                highestId = container.GameHistoryAqours.Max(x => ulong.Parse(x.Id));
                
                //if we have more than 10 entries, remove the one with the lowest id
                if (container.GameHistoryAqours.Count > 10)
                {
                    container.GameHistoryAqours.Remove(container.GameHistoryAqours.First());
                }                break;

            //saint snow
            case 2:
                if (container.GameHistorySaintSnow.Count == 0) break;

                highestId = container.GameHistorySaintSnow.Max(x => ulong.Parse(x.Id));
                
                //if we have more than 10 entries, remove the one with the lowest id
                if (container.GameHistorySaintSnow.Count > 10)
                {
                    container.GameHistorySaintSnow.Remove(container.GameHistorySaintSnow.First());
                }                break;
        }
        
        //todo: use a mapper lol
        GameHistoryBase newHistory = new()
        {
            Id = highestId.ToString(),
            PlayPlace = "test",
            Created = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"),
            DUserId = session.User.UserId.ToString(),
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
            SkillArgCamera = gameResult.SkillArgCamera,
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
            TechnicalRank = gameResult.TechnicalRank,
            SkillRank = gameResult.SkillRank,
            SynchroRank = gameResult.SynchroRank,
            ComboRank = gameResult.ComboRank,
            TotalRank = gameResult.TotalRank,
            Favorite = false,
            PrintRest = 1,
            MemorialCard = gameResult.MemorialCard,
            LastCutFocus = gameResult.LastCutFocus,
            RecommendHiScore = false,
            RecommendFirstMusic = false,
            RecommendFirstMember = false,
            RecommendFirstSkill = false,
        };
        
        //todo: move away from reflectionmapper here because it is inefficient
        switch (container.UserData.IdolKind)
        {
            //µ's
            case 0:
                container.GameHistory.Add(ReflectionMapper.Map(newHistory, new GameHistory()));
                break;

            //aqours
            case 1:
                container.GameHistoryAqours.Add(ReflectionMapper.Map(newHistory, new GameHistoryAqours()));
                break;

            //saint snow
            case 2:
                container.GameHistorySaintSnow.Add(ReflectionMapper.Map(newHistory, new GameHistorySaintSnow()));
                break;
        }
        
        //update level data
        container.UserData.TotalExp = gameResult.TotalExp;
        container.UserData.Level = gameResult.DUserLevel;
        
        //write to db
        await container.SaveChanges(cancellationToken);

        return new ResponseContainer
        {
            Result = 200,
            Response = new GameResultResponse
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