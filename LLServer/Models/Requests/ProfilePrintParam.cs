using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

/*
    "param": {
        "camera_rotate_xz": 0,
        "camera_rotate_y": 0,
        "camera_zoom": 0,
        "dice_bonus": 0,
        "frame_design": 1007,
        "frame_rotate": 0,
        "game_history_id": "1",
        "mode": 0,
        "motion_frame": 0,
        "print_mode": 1
    },
    "protocol": "profile.print",
 */

public class ProfilePrintParam
{
    [JsonPropertyName("camera_rotate_xz")]
    public float CameraRotateXz { get; set; }
    
    [JsonPropertyName("camera_rotate_y")]
    public float CameraRotateY { get; set; }
    
    [JsonPropertyName("camera_zoom")]
    public int CameraZoom { get; set; }
    
    [JsonPropertyName("dice_bonus")]
    public int DiceBonus { get; set; }
    
    [JsonPropertyName("frame_design")]
    public int FrameDesign { get; set; }
    
    [JsonPropertyName("frame_rotate")]
    public int FrameRotate { get; set; }
    
    [JsonPropertyName("game_history_id")]
    public string GameHistoryId { get; set; } = "";
    
    [JsonPropertyName("mode")]
    public int Mode { get; set; }
    
    [JsonPropertyName("motion_frame")]
    public int MotionFrame { get; set; }
    
    [JsonPropertyName("print_mode")]
    public int PrintMode { get; set; }
}