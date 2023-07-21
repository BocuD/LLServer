using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class YellAchievement
{
    [JsonPropertyName("m_yell_achievement_id")]
    public int YellAchievementId { get; set; }

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; }

    [JsonPropertyName("new")]
    public bool New { get; set; }
    
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}