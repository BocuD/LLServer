using System.Text.Json.Serialization;
using LLServer.Common;
using LLServer.Database;
using LLServer.Event;
using LLServer.Event.Database;
using LLServer.Formatters;
using LLServer.Gacha;
using LLServer.Gacha.Database;
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
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllers(
        options => options.OutputFormatters.Insert(0, new BinaryMediaTypeFormatter()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
    });

//Don't serialize null values
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.OperationFilter<DebugHeaderFilter>());
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath | HttpLoggingFields.RequestQuery | HttpLoggingFields.RequestMethod;
    logging.RequestBodyLogLimit = 4096;
});
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

//load databases
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("DataSource=test.db3");
});
builder.Services.AddDbContext<EventDbContext>(options =>
{
    options.UseSqlite("DataSource=events.db3");
});
builder.Services.AddDbContext<GachaDbContext>(options =>
{
    options.UseSqlite("DataSource=gacha.db3");
});

builder.Services.AddScoped<SessionHandler>();
builder.Services.AddScoped<EventDataProvider>();
builder.Services.AddScoped<GachaDataProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Log.Information("Migrating user database...");
    userDb.Database.Migrate();
    
    var eventDb = scope.ServiceProvider.GetRequiredService<EventDbContext>();
    Log.Information("Migrating event database...");
    eventDb.Database.Migrate();
    
    var eventDataProvider = scope.ServiceProvider.GetRequiredService<EventDataProvider>();
    Log.Information("Loading events...");
    eventDataProvider.CacheEvents();
    
    var gachaDb = scope.ServiceProvider.GetRequiredService<GachaDbContext>();
    Log.Information("Migrating gacha database...");
    gachaDb.Database.Migrate();
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
    // //prevent requests from outside local network
    // if (context.Connection.RemoteIpAddress != null && !context.Connection.RemoteIpAddress.ToString().Contains("192.168.178.1"))
    // {
    //     context.Response.StatusCode = 403;
    //     return;
    // }
    
    await next();
    
    if (context.Response.StatusCode >= 400)
    {
        Log.Error("Unknown request from: {RemoteIpAddress} {Method} {Path} {StatusCode}",
            context.Connection.RemoteIpAddress, context.Request.Method, context.Request.Path, context.Response.StatusCode);
    }
    else
    {
        Log.Information($"Handled request: {context.Request.Method} {context.Request.Path} returned {context.Response.StatusCode} from: {context.Connection.RemoteIpAddress}");
    }
});

app.MapControllers();

app.Run();