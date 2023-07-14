using System.Text.Json.Serialization;
using LLServer.Models.UserDataModel;

namespace LLServer.Models;

public class ResponseContainer
{
    [JsonPropertyName("result")] 
    public int Result { get; set; } = 200;

    [JsonPropertyName("response")] 
    public required ResponseBase Response { get; set; }
}

[JsonDerivedType(typeof(BasicInfo))]
[JsonDerivedType(typeof(InformationResponse))]
[JsonDerivedType(typeof(AuthResponse))]
[JsonDerivedType(typeof(GameEntryResponse))]
[JsonDerivedType(typeof(UserDataResponse))]
[JsonDerivedType(typeof(RankingResponse))]
public class ResponseBase
{
    
}