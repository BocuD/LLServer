using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace LLServer.Formatters;

public class BinaryMediaTypeFormatter : OutputFormatter
{
    private static readonly Type SUPPORTED_TYPE = typeof(byte[]);

    public BinaryMediaTypeFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/octet-stream"));
    }

    protected override bool CanWriteType(Type? type)
    {
        return type == SUPPORTED_TYPE;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
        var value = Array.Empty<byte>();
        if (context.Object is byte[] bytes)
        {
            value = bytes;
        }

        await context.HttpContext.Response.BodyWriter.WriteAsync(value);
    }
}