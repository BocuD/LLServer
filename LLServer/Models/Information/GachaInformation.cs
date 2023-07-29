using System.Text.Json.Serialization;

namespace LLServer.Models.Information;

public class GachaInformation
{
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonPropertyName("gacha_list")]
    public List<GachaInformationItem> GachaList { get; set; } = new();
}

public class GachaInformationItem
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    
    [JsonPropertyName("idol_kind")]
    public required int IdolKind { get; set; }
    
    [JsonPropertyName("gacha_type")]
    public required int GachaType { get; set; }
    
    [JsonPropertyName("character_id")]
    public required int CharacterId { get; set; }
    
    [JsonPropertyName("priority")]
    public required int Priority { get; set; }
    
    [JsonPropertyName("draw_limit")]
    public required int DrawLimit { get; set; }
    
    [JsonPropertyName("cracker_enable")]
    public required bool CrackerEnable { get; set; }
    
    [JsonPropertyName("is_event")]
    public required bool IsEvent { get; set; }
    
    [JsonPropertyName("first_only")]
    public required bool FirstOnly { get; set; }

    [JsonPropertyName("rates")] 
    public List<int> Rates { get; set; } = new() { 0, 1, 2 };
    
    [JsonPropertyName("resource")]
    public required string Resource { get; set; } = string.Empty;
    
    [JsonPropertyName("gacha_id")]
    public required string GachaId { get; set; } = string.Empty;
    
    [JsonPropertyName("image")]
    public required string Image { get; set; } = string.Empty;
    
    [JsonPropertyName("image_event")]
    public required string ImageEvent { get; set; } = string.Empty;
    
    [JsonPropertyName("title")]
    public required string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("caption")]
    public required string Caption { get; set; } = string.Empty;
    
    [JsonPropertyName("menu_caption")]
    public required string MenuCaption { get; set; } = string.Empty;
    
    [JsonPropertyName("start_datetime")]
    public required string StartDatetime { get; set; }
    
    [JsonPropertyName("end_datetime")]
    public required string EndDatetime { get; set; }
    
    [JsonPropertyName("start_menu_datetime")]
    public required string StartMenuDatetime { get; set; }
    
    [JsonPropertyName("end_menu_datetime")]
    public required string EndMenuDatetime { get; set; }
    
    [JsonPropertyName("draw_items")]
    public DrawItems DrawItems { get; set; } = new();
}

public class DrawItems
{
    [JsonPropertyName("normal")] 
    public DrawItem Normal { get; set; } = new DrawItem();

    [JsonPropertyName("first")] 
    public DrawItem First { get; set; } = new DrawItem();
}

public class DrawItem
{
    [JsonPropertyName("item_id")] 
    public int ItemId { get; set; }

    [JsonPropertyName("level")] 
    public int Level { get; set; }

    [JsonPropertyName("category")] 
    public int Category { get; set; }
}