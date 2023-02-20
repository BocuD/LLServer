using System.Text.Json.Serialization;

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
public class ResponseBase
{
    
}