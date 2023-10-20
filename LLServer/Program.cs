using LLServer.Common;
using LLServer.Database;
using LLServer.Event;
using LLServer.Formatters;
using LLServer.Middlewares;
using LLServer.Session;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting server...");

builder.Host.UseSerilog();

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
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("DataSource=test.db3");
    
    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.None));

    options.UseLoggerFactory(loggerFactory);
});
builder.Services.AddScoped<SessionHandler>();
builder.Services.AddScoped<EventDataProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpLogging();
app.UseWhen(context => context.Request.Path.StartsWithSegments("/game"),
    applicationBuilder => applicationBuilder.UseMiddleware<AesMiddleware>());

//Log unhandled requests
app.Use(async (context, next) =>
{
    await next();
    
    if (context.Response.StatusCode >= 400)
    {
        Log.Error("Unknown request from: {RemoteIpAddress} {Method} {Path} {StatusCode}",
            context.Connection.RemoteIpAddress, context.Request.Method, context.Request.Path, context.Response.StatusCode);
    }
    else
    {
        Log.Information($"Handled request: {context.Request.Method} {context.Request.Path} returned {context.Response.StatusCode}");
    }
});

app.MapControllers();

app.Run();