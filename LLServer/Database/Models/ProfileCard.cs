using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LLServer.Models.UserData;

namespace LLServer.Database.Models;

public class ProfileCard
{ 
    [Key] public string ProfileCardId { get; set; }

    //Database association to game history
    [ForeignKey("GameHistory")]
    public int GameHistoryId { get; set; }
    public GameHistoryBase? GameHistory { get; set; }
    
    //Database association to user
    [ForeignKey("User")] 
    public ulong UserID { get; set; }
    public User? User { get; set; }
}