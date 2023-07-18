using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

namespace LLServer.Models.UserData;

public class UserDataSaintSnow : UserDataBase
{
    [JsonIgnore, Key, ForeignKey("User")] public ulong UserID { get; set; }
    
    //One to one relationship with user
    [JsonIgnore] public User? User { get; set; }
}