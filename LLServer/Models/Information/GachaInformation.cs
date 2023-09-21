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
    
    
    //1: birthday
    //3: member gacha
    [JsonPropertyName("gacha_type")]
    public required int GachaType { get; set; }
    
    [JsonPropertyName("character_id")]
    public required int CharacterId { get; set; }
    
    [JsonPropertyName("priority")]
    public required int Priority { get; set; }
    
    [JsonPropertyName("draw_limit")]
    public required int DrawLimit { get; set; }
    
    [JsonPropertyName("cracker_enable")]
    public required int CrackerEnable { get; set; } //bool
    
    [JsonPropertyName("is_event")]
    public required int IsEvent { get; set; } //bool
    
    [JsonPropertyName("first_only")]
    public required int FirstOnly { get; set; } //bool

    [JsonPropertyName("rates")] 
    public List<int> Rates { get; set; } = new();
    
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
    
    //[JsonPropertyName("draw_items")]
    //public DrawItems DrawItems { get; set; } = new();

    public static GachaInformationItem DummyGachaInfo()
    {
        return new GachaInformationItem
        {
            Id = 1,
            IdolKind = 0,
            GachaType = 3, //thank you tung..?
            CharacterId = 4,
            Priority = 1,
            DrawLimit = 2,
            CrackerEnable = 1, //true
            IsEvent = 0, //false
            FirstOnly = 0, //false
            Rates = new List<int> { 0, 1, 2 },
            Resource = "426",
            GachaId = "774",
            Image = "info_774_2.jpg",
            ImageEvent = "info_774_2.jpg",
            Title = "yes",
            Caption = "a caption",
            MenuCaption = "a menu caption",
            StartDatetime = DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss"),
            EndDatetime = (DateTime.Now + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
            StartMenuDatetime = DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss"),
            EndMenuDatetime = (DateTime.Now + TimeSpan.FromDays(3650)).ToString("yyyy-MM-ddhh:mm:ss"),
            /*DrawItems = new DrawItems
            {
                First = new DrawItem
                {
                    Category = 0,
                    ItemId = 0,
                    Level = 0
                },
                Normal = new DrawItem
                {
                    Category = 0,
                    ItemId = 0,
                    Level = 0
                }
            }*/
        };
    }
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