using System.Text.Json.Serialization;
using LLServer.Models.Responses.Gacha;
using LLServer.Models.Responses.Terminal;
using LLServer.Models.Responses.Travel;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.Responses;

public class ResponseContainer
{
    [JsonPropertyName("result")]
    public int Result { get; set; } = 200;

    [JsonPropertyName("response")]
    public required ResponseBase Response { get; set; }
}

[JsonDerivedType(typeof(BasicInfoResponse))]
[JsonDerivedType(typeof(InformationResponse))]
[JsonDerivedType(typeof(AuthResponse))]
[JsonDerivedType(typeof(GameEntryResponse))]
[JsonDerivedType(typeof(UserDataResponse))]
[JsonDerivedType(typeof(RankingResponse))]
[JsonDerivedType(typeof(GameConfigResponse))]
[JsonDerivedType(typeof(GameResultResponse))]
[JsonDerivedType(typeof(TravelStartResponse))]
[JsonDerivedType(typeof(TravelResultResponse))]
[JsonDerivedType(typeof(TravelStampResponse))]
[JsonDerivedType(typeof(AchievementResponse))]
[JsonDerivedType(typeof(AchievementYellResponse))]
[JsonDerivedType(typeof(MusicUnlockResponse))]
[JsonDerivedType(typeof(TravelPrintResponse))]
[JsonDerivedType(typeof(MemberGachaResponse))]
[JsonDerivedType(typeof(GetMemberCardResponse))]
public class ResponseBase
{
}