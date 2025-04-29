namespace Sports.Api.Web;

using FastEndpoints;
using Sports.Api.Service.Espn;
using Sports.Api.Service.Interface.Espn;
using Sports.Api.Service.Interface;
using Sports.Api.Service;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.Extensions.Options;
using Sports.Api.Web.Config;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton(typeof(IEspnApiService<>), typeof(EspnApiService<>));
        builder.Services.AddSingleton(typeof(IEspnCacheItemService<>), typeof(EspnCacheItemService<>));
        builder.Services.AddSingleton<IEspnCompetitionService, EspnCompetitionService>();
        builder.Services.AddSingleton<IEspnCompetitorsService, EspnCompetitorsService>();
        builder.Services.AddSingleton<IEspnEventService, EspnEventService>();
        builder.Services.AddSingleton<IEspnLeagueService, EspnLeagueService>();
        builder.Services.AddSingleton<IEspnSportService, EspnSportService>();
        builder.Services.AddSingleton<IEspnTeamService, EspnTeamService>();
        builder.Services.AddSingleton<IEspnWeekEventService, EspnWeekEventService>();
        builder.Services.AddSingleton<IEspnWeekService, EspnWeekService>();

        builder.Services.AddSingleton<ICacheService, CacheService>();
        builder.Services.AddSingleton<IMemoryCacheService, MemoryCacheService>();

        builder.Services.AddSingleton<IGetSportService, GetSportService>();
        builder.Services.AddSingleton<IGetSportLeagueService, GetSportLeagueService>();

        builder
            .Services
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddCors();

        builder
            .Services
            .AddMemoryCache()
            .AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            })
            .Configure<GzipCompressionProviderOptions>(options => 
            {
                options.Level = CompressionLevel.Optimal;
            });

        var app = builder.Build();
        
        var corsConfig = builder
            .Configuration
            .GetRequiredSection("Cors")
            .Get<CorsConfig>()!;

        app
            .UseFastEndpoints()
            .UseSwaggerGen()
            .UseResponseCompression()
            .UseCors(corsBuilder => corsBuilder.WithOrigins(corsConfig.AllowedOrigins));

        app.Run();
    }
}
