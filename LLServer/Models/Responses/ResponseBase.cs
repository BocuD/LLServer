using System.Text.Json.Serialization;

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
public class ResponseBase
{
    
}