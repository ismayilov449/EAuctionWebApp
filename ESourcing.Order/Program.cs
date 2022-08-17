using ESourcing.Ordering.Infrastructure.Infrastructure;
using ESourcing.Orders.Consumers;
using ESourcing.Orders.Extensions;
using ESouring.Ordering.Application.Infrastructure;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var webHost = builder.WebHost;

builder.Services.AddControllers();

#region Load Balancing

services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

#endregion

#region Project Dependencies

services.AddInfrastuctureDependencies(builder.Configuration);
services.AddApplicationDependencies();
services.AddAutoMapper(typeof(Program));

#endregion

#region Event Bus
services.AddSingleton<IRabbitMQPersistentConnection>(s =>
{
    var logger = s.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"],
    };

    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
    }

    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
    {
        factory.Password = builder.Configuration["EventBus:Password"];
    }

    var retryCount = 5;

    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:RetryCount"]))
    {
        retryCount = int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }

    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);

});

services.AddSingleton<EventBusOrderCreateConsumer>();

#endregion

#region Redis Cache

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379,abortConnect=False";
});
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.UseEventBusListener();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.ContentType = "text/plain";

        // Host info
        var name = Dns.GetHostName(); // get container id
        var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        Console.WriteLine($"Host Name: {Environment.MachineName} \t {name}\t {ip}");
        await context.Response.WriteAsync($"Host Name: {Environment.MachineName}{Environment.NewLine}");
        await context.Response.WriteAsync(Environment.NewLine);

        // Request method, scheme, and path
        await context.Response.WriteAsync($"Request Method: {context.Request.Method}{Environment.NewLine}");
        await context.Response.WriteAsync($"Request Scheme: {context.Request.Scheme}{Environment.NewLine}");
        await context.Response.WriteAsync($"Request URL: {context.Request.GetDisplayUrl()}{Environment.NewLine}");
        await context.Response.WriteAsync($"Request Path: {context.Request.Path}{Environment.NewLine}");

        // Headers
        await context.Response.WriteAsync($"Request Headers:{Environment.NewLine}");
        foreach (var (key, value) in context.Request.Headers)
        {
            await context.Response.WriteAsync($"\t {key}: {value}{Environment.NewLine}");
        }
        await context.Response.WriteAsync(Environment.NewLine);

        // Connection: RemoteIp
        await context.Response.WriteAsync($"Request Remote IP: {context.Connection.RemoteIpAddress}");
    });
});

app.Run();
