using System.Text.Json.Serialization;

namespace LLServer.Models;

public class RankingResponse : ResponseBase
{
    [JsonPropertyName("ranking")] public RankingData[] Ranking { get; set; } = new RankingData[0];
    [JsonPropertyName("coop_ranking")] public CoopRanking[] CoopRanking { get; set; } = new CoopRanking[0];
    [JsonPropertyName("music_ranking")] public MusicRanking[] MusicRanking { get; set; } = new MusicRanking[0];
    [JsonPropertyName("music_total")] public int MusicTotal { get; set; }
    [JsonPropertyName("member_ranking")] public MemberRanking[] MemberRanking { get; set; } = new MemberRanking[0];
    [JsonPropertyName("yell_ranking")] public YellRanking[] YellRanking { get; set; } = new YellRanking[0];

    public static RankingResponse DummyRankingResponse()
    {
        RankingData[] dummyRankingData = new RankingData[10];

        for (int index = 0; index < dummyRankingData.Length; index++)
        {
            dummyRankingData[index] = new RankingData
            {
                Rank = index + 1, MusicId = index, Score = 69420
            };
        }

        MemberRanking[] dummyMemberRanking = new MemberRanking[10];

        for (int index = 0; index < dummyMemberRanking.Length; index++)
        {
            dummyMemberRanking[index] = new MemberRanking
            {
                Rank = index + 1, CharacterId = index, Score = 69420
            };
        }


        RankingResponse response = new()
        {
            Ranking = dummyRankingData,
            CoopRanking = dummyMemberRanking.Select(rankingData => new CoopRanking
            {
                CharacterId = rankingData.CharacterId,
                Rank = rankingData.Rank,
                Score = rankingData.Score
            }).ToArray(),

            MusicRanking = dummyRankingData.Select(rankingData => new MusicRanking
            {
                MusicId = rankingData.MusicId,
                Rank = rankingData.Rank,
                Score = rankingData.Score
            }).ToArray(),

            MusicTotal = 10,
            MemberRanking = dummyMemberRanking,
            YellRanking = dummyMemberRanking.Select(rankingData => new YellRanking
            {
                Rank = rankingData.Rank,
                Score = rankingData.Score,
                Badge = 0,
                Honor = 0,
                Name = "Test      ",
                Nameplate = 0,
                TenpoName = "1337",
                Uid = "1234567890"
            }).ToArray()
        };

        return response;
    }
}