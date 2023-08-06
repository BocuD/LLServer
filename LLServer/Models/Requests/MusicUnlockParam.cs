using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
{
    "blockseq": 14,
    "game": {
        "eventcode": "000",
        "version": "2.4.1"
    },
    "param": {
        "music_id": 380
    },
    "protocol": "music.unlock",
    "sessionkey": "7aa53f815c344f3abd5df27316b43a36",
    "terminal": {
        "tenpo_id": "1337",
        "tenpo_index": 1337,
        "terminal_attrib": 0,
        "terminal_id": "309C231D4B94"
    }
}
 */

public class MusicUnlockParam
{
    [JsonPropertyName("music_id")]
    public int MusicId { get; set; }
}