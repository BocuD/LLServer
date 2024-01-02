using System.ComponentModel.DataAnnotations;

namespace LLServer.Event.Database;

public class EventEntry : InformationEntryBase
{
    [Key] public int id { get; set; }
    
    public bool Active { get; set; }
    public int EventType { get; set; }
    public int PointType { get; set; }
    public int CharacterId { get; set; }
    public float PointMag { get; set; }
    public int MemberTravelPamphletId { get; set; }
}