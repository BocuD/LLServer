using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LLServer.Event.Database;

public class InformationEntryBase
{
    public string name { get; set; }
    
    [NotMapped] public string startString { get => start.ToString("yyyy-MM-dd"); set => start = DateTime.Parse(value); }
    [NotMapped] public string endString { get => end.ToString("yyyy-MM-dd"); set => end = DateTime.Parse(value); }
    
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    
    [NotMapped] public int dayCount { get => (end - start).Days; }

    [ForeignKey("resource")] public int resourceID { get; set; }
    [JsonIgnore] public ResourceEntry resource { get; set; }
}

public class InformationEntry : InformationEntryBase
{
    [Key] public int id { get; set; }
    
    public bool DisplayCenter { get; set; }
    public bool DisplaySatellite { get; set; }
}
