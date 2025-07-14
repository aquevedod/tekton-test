using Serilog;
using System.Diagnostics;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Caching;
using ProductApi.Infrastructure.ExternalServices;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var discountServiceUrl = builder.Configuration["ExternalServices:DiscountService:BaseUrl"];
var performanceLogger = new LoggerConfiguration()
    .WriteTo.File("Logs/performance.txt", 
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Message}{NewLine}")
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

builder.Services.AddMediatR(Assembly.Load("ProductApi.Application"));

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductStatusCache, ProductStatusCache>();

builder.Services.Configure<DiscountServiceOptions>(
    builder.Configuration.GetSection(DiscountServiceOptions.SectionName));

builder.Services.AddHttpClient<IDiscountService, DiscountService>();

builder.Services.AddMemoryCache();

builder.Services.AddKeyedSingleton<Serilog.ILogger>("PerformanceLogger", performanceLogger);

var app = builder.Build();

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();
    await next();
    stopwatch.Stop();

    var elapsedMs = stopwatch.ElapsedMilliseconds;
    var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode} in {elapsedMs} ms";

    var performanceLogger = context.RequestServices.GetKeyedService<Serilog.ILogger>("PerformanceLogger");
    performanceLogger?.Information(logMessage);
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseAuthorization();
app.MapControllers();
app.Run();