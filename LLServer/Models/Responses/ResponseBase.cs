using System.Text.Json.Serialization;
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
public class ResponseBase
{
}