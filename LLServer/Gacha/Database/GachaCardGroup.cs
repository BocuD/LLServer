using System.ComponentModel.DataAnnotations;

namespace LLServer.Gacha.Database;

public class GachaCardGroup
{
    [Key] public int id { get; set; }
    
    public string name { get; set; }

    public string[] cardIds { get; set; }
}