﻿using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class MailBoxItem
{
    [JsonPropertyName("id")] 
    public string Id { get; set; }

    [JsonPropertyName("attrib")]
    public int Attrib { get; set; }     //purpose unclear; 0 seems to just work™

    [JsonPropertyName("category")]
    public int Category { get; set; }   //1 might be member card (who knows); 2 seems to be skill card
    
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; } 
    
    [JsonPropertyName("count")]
    public int Count { get; set; } 
}