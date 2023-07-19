using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LLServer.Database.Models;

public class PersistentLiveData
{
    //Database key
    [JsonIgnore, Key] public int Id { get; set; }
    
    public int LiveId { get; set; }
    public int SelectCount { get; set; }
    public bool Unlocked { get; set; }
    public bool New { get; set; }
    public int FullCombo { get; set; }
    public int TotalHiScore { get; set; }
    public int TechnicalHiScore { get; set; }
    public int TechnicalHiRate { get; set; } 
    
    /*
    public int CoopTotalHiScore2 { get; set; } 
    public int CoopTotalHiScore3 { get; set; }
    */
    public int PlayerCount1 { get; set; }
    /*
    public int PlayerCount2 { get; set; } 
    public int PlayerCount3 { get; set; } 
    public int RankCount0 { get; set; } 
    public int RankCount1 { get; set; } 
    public int RankCount2 { get; set; } 
    public int RankCount3 { get; set; } 
    public int RankCount4 { get; set; } 
    public int RankCount5 { get; set; } 
    public int RankCount6 { get; set; }
    */
    public int TrophyCountGold { get; set; } 
    public int TrophyCountSilver { get; set; } 
    public int TrophyCountBronze { get; set; } 
    public int FinaleCount { get; set; }
    public int TechnicalRank { get; set; }
    
    
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    
    //One to one relationship with user
    [JsonIgnore] public User? User { get; set; }
}