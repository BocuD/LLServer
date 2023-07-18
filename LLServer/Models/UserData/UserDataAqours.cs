using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class UserDataAqours : UserDataBase
{
    [JsonIgnore, Key, ForeignKey("User")] public ulong UserID { get; set; }
    
    //One to one relationship with user
    [JsonIgnore] public User? User { get; set; }
}