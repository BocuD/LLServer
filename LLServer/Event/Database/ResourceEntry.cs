using System.ComponentModel.DataAnnotations;

namespace LLServer.Event.Database;

public class ResourceEntry
{
    [Key] public int id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public string hash { get; set; }

    // public ResourceEntry(string path)
    // {
    //     this.path = path;
    //     
    //     //load file and compute hash
    //     using MD5 md5 = MD5.Create();
    //     using FileStream stream = File.OpenRead(path);
    //     byte[] hashBytes = md5.ComputeHash(stream);
    //     hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    // }
}