using System.Security.Cryptography;

namespace LLServer.Event;

public class ResourceEntry
{
    public readonly string path;
    public readonly string hash;

    public ResourceEntry(string path)
    {
        this.path = path;
        
        //load file and compute hash
        using MD5 md5 = MD5.Create();
        using FileStream stream = File.OpenRead(path);
        byte[] hashBytes = md5.ComputeHash(stream);
        hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}