using System.ComponentModel.DataAnnotations;
using LLServer.Models.UserData;

namespace LLServer.Database.Models;

public class User
{
    [Key]
    public ulong UserId { get; set; }
    
    public string CardId { get; set; } = "7020392000000000";

    public bool Initialized { get; set; }
    
    public Session? Session { get; set; }

    //user data
    [Required]
    public UserData? UserData { get; set; }
    [Required]
    public UserDataAqours? UserDataAqours { get; set; }
    [Required]
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    //member data
    [Required] public List<MemberData> Members { get; set; } = new();
    [Required] public List<MemberCardData> MemberCards { get; set; } = new();
    
    //score and unlock data
    [Required] public List<PersistentLiveData> LiveDatas { get; set; } = new();
    
    //other data
    public string Flags { get; set; } = "";
}