using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class UserDataBase
{
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("honor")]
    public int Honor { get; set; }

    [JsonPropertyName("badge")]
    public int Badge { get; set; }

    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }

    [JsonIgnore] [NotMapped]
    public ProfileCard ProfileCard1 { get; set; } = new(0);

    [JsonPropertyName("profile_card_id_1")]
    public string ProfileCardId1
    {
        get => ProfileCard1.ToString();
        set => ProfileCard1 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }

    [JsonIgnore] [NotMapped]
    public ProfileCard ProfileCard2 { get; set; } = new(0);

    [JsonPropertyName("profile_card_id_2")]
    public string ProfileCardId2
    {
        get => ProfileCard2.ToString();
        set => ProfileCard2 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }
}