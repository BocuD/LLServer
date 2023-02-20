using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LLServer.Middlewares;

public class DebugHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters is null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }
        
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Debug",
            In = ParameterLocation.Header,
            Required = false,
            Description = "Debug header for use to disable aes"
        });
    }
}