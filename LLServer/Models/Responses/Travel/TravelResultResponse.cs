using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses.Travel;

public class TravelResultResponse : ResponseBase
{
    [JsonPropertyName("get_card_datas")]
    public GetCardData[] GetCardDatas { get; set; } = Array.Empty<GetCardData>();
    
    //same format as user id (20 digit long)
    [JsonPropertyName("travel_history_ids")]
    public string[] TravelHistoryIds { get; set; } = Array.Empty<string>();
    
    [JsonPropertyName("mailbox")]
    public MailBoxItem[] MailBox { get; set; } = Array.Empty<MailBoxItem>();
}

public class GetCardData
{
    [JsonPropertyName("mailbox_id")] public string MailBoxId { get; set; }
    [JsonPropertyName("location")] public int Location { get; set; }
}