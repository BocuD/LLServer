using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class EventResultReward
{
    [JsonPropertyName("reward")]
    public EventResultRewardData Reward { get; set; } = new();

    [JsonPropertyName("next_reward")]
    public EventResultRewardData NextReward { get; set; } = new();

    public class EventResultRewardData
    {
        [JsonPropertyName("require_point")]
        public int RequirePoint { get; set; }

        [JsonPropertyName("reward_type")]
        public int RewardType { get; set; }

        [JsonPropertyName("reward_arg")]
        public int RewardArg { get; set; }
    }
}