﻿using System.Text.Json.Serialization;
using LLServer.Models.Information;

namespace LLServer.Models;


// TODO: Figure out all the fields
public class InformationResponse : ResponseBase
{
    /// <summary>
    /// Probably next request url? Not entirely sure
    /// </summary>
    [JsonPropertyName("base_url")]
    public required string BaseUrl { get; set; }
    
    [JsonPropertyName("information")]
    public List<Information.Information> InformationItems { get; set; } = new();

    [JsonPropertyName("event_information")]
    public List<Information.EventInformation> EventInformationItems { get; set; } = new();

    /// <summary>
    /// Date string yyyy-MM-dd
    /// </summary>
    [JsonPropertyName("encore_expiration_date")]
    public required string EncoreExpirationDate { get; set; }

    [JsonPropertyName("sif_music_information")]
    public List<MusicInformation> MusicInformationItems { get; set; } = new();

    [JsonPropertyName("resource_information")]
    public List<ResourceInformation> ResourceInformationItems { get; set; } = new();
}