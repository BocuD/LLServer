using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LLServer.Event.Database;

public class InformationEntryBase
{
    public string name { get; set; }
    
    public DateTime start { get; set; }
    public DateTime end { get; set; }

    [ForeignKey("resource")] public int resourceID { get; set; }
    public ResourceEntry resource { get; set; }
}

public class InformationEntry : InformationEntryBase
{
    [Key] public int id { get; set; }
    
    public bool DisplayCenter { get; set; }
    public bool DisplaySatellite { get; set; }
}