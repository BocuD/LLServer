using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class SifPrint
{
    [JsonPropertyName("m_sif_print_card_id")]
    public int CardId { get; set; } = 0;
    
    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } = false;
    
    [JsonPropertyName("print_rest")]
    public int PrintRest { get; set; } = 1;
}