using Serilog;
using Serilog.Events;
using System.Diagnostics;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Caching;
using ProductApi.Infrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/requests.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

builder.Services.AddMediatR(Assembly.Load("ProductApi.Application"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductStatusCache, ProductStatusCache>();

builder.Services.Configure<DiscountServiceOptions>(
    builder.Configuration.GetSection(DiscountServiceOptions.SectionName));

builder.Services.AddScoped<IDiscountService, MockDiscountService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();
    await next();
    stopwatch.Stop();

    var elapsedMs = stopwatch.ElapsedMilliseconds;
    var logMessage = $"[{DateTime.Now}] {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode} in {elapsedMs} ms";

    Log.Information(logMessage);
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