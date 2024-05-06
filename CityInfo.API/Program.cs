using CityInfo.API.DbContexts;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.log", rollingInterval: RollingInterval.Hour) //每一小時重新產新新的檔案
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(r =>

    //https://learn.microsoft.com/zh-tw/aspnet/core/web-api/advanced/formatting?view=aspnetcore-8.0
    //如果找不到符合用戶端要求的格式器，ASP.NET Core：則傳回 406 Not Acceptable
    r.ReturnHttpNotAcceptable = true
).AddXmlDataContractSerializerFormatters(); //新增 XML 格式支援

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 统一处理异常，为不同的异常设置不同的信息

builder.Services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx =>
{
    ctx.ProblemDetails.Extensions.Add("additionalMetadata", "customValue");
    ctx.ProblemDetails.Extensions.Add("MachineName", Environment.MachineName);
});

#endregion

#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMainService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>(optionsBuilder =>
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("CityInfoContextConnection")));

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

var app = builder.Build();

//
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
