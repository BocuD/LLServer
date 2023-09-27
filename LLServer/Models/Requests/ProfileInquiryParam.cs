using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": {
        "do_relation": 1,
        "profile_card_id": "000102030405060708090a0b0c0d0e0f"
    },
    "protocol": "profile.inquiry",
 */

public class ProfileInquiryParam
{
    [JsonPropertyName("do_relation")]
    public int DoRelation { get; set; } = 1;
    
    [JsonPropertyName("profile_card_id")]
    public string ProfileCardId { get; set; } = "";
}