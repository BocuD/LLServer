using System.Security.Cryptography;
using LLServer.Common;
using Throw;

namespace LLServer.Middlewares;

public class AesMiddleware
{
    private readonly RequestDelegate next;

    public AesMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var isDebug = context.Request.Headers.ContainsKey("Debug");
        Stream? originBody = null;

        var aes = Aes.Create();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Key = CryptoConstants.AES_KEY_BYTES;
        aes.IV = CryptoConstants.IV_BYTES;
        aes.FeedbackSize = 128;
        aes.Mode = CipherMode.CFB;
        aes.Padding = PaddingMode.PKCS7;
        var encryptTransform = aes.CreateEncryptor();
        var decryptTransform = aes.CreateDecryptor();

        if (!isDebug)
        {
            var result = new MemoryStream();

            await using var cryptoStream = new CryptoStream(request.Body, decryptTransform, CryptoStreamMode.Read);
            await cryptoStream.CopyToAsync(result);
            request.Body = result;
            result.Position = 0;
            originBody = ReplaceBody(context.Response);
        }

        await next.Invoke(context);

        if (!isDebug)
        {
            var expected = context.Response.Body.Length;
            var responseBody = new byte[expected];
            context.Response.Body.Position = 0;
            var readBytes = await context.Response.Body.ReadAsync(responseBody);
            readBytes.Throw().IfNotEquals((int)expected);
            await using (var output = new MemoryStream())
            {
                var cryptoStream =
                    new CryptoStream(output, encryptTransform, CryptoStreamMode.Write);

                await cryptoStream.WriteAsync(responseBody);
                await cryptoStream.FlushFinalBlockAsync();

                output.Position = 0;
                context.Response.Body.Position = 0;
                await output.CopyToAsync(context.Response.Body);
                context.Response.ContentLength = context.Response.Body.Length;
            }

            await ReturnBody(context.Response, originBody!);
        }
    }

    private static Stream ReplaceBody(HttpResponse response)
    {
        var originBody = response.Body;
        response.Body = new MemoryStream();
        return originBody;
    }

    private static async Task ReturnBody(HttpResponse response, Stream originBody)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        await response.Body.CopyToAsync(originBody);
        response.Body = originBody;
    }
}