using LLServer.Common;
using LLServer.Formatters;
using LLServer.Middlewares;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options => options.OutputFormatters.Insert(0, new BinaryMediaTypeFormatter()))
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.OperationFilter<DebugHeaderFilter>());
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath | HttpLoggingFields.RequestQuery | HttpLoggingFields.RequestMethod;
    logging.RequestBodyLogLimit = 4096;
});
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.MapControllers();
app.UseWhen(context => context.Request.Path.StartsWithSegments("/game"), 
    applicationBuilder => applicationBuilder.UseMiddleware<AesMiddleware>());

app.Run();