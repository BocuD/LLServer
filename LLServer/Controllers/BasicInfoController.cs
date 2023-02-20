using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers;

[ApiController]
[Route("[controller]")]
public class BasicInfoController : ControllerBase
{
    [HttpPost]
    public ActionResult<BasicInfo> GetBasicInfo()
    {
        var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.KeySize = 128;
        aes.GenerateKey();
        var key = BitConverter.ToString(aes.Key).Replace("-", "");

        var info = new BasicInfo
        {
            Response = new BasicInfoResponse
            {
                BaseUrl = "http://127.0.0.1:1234",
                DownloadUrl = "http://127.0.0.1:1234",
                Key = key,
                Iv = "0000000000000000",
                TenpoIndex = 1
            }
        };

        return Ok(info);
    }
}