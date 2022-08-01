using ESourcing.Ordering.Infrastructure.Infrastructure;
using ESourcing.Orders.Consumers;
using ESourcing.Orders.Extensions;
using ESouring.Ordering.Application.Infrastructure;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producers;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Project Dependencies

builder.Services.AddInfrastuctureDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies();

#endregion


#region Event Bus
builder.Services.AddSingleton<IRabbitMQPersistentConnection>(s =>
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

builder.Services.AddSingleton<EventBusOrderCreateConsumer>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.UseEventBusListener();

app.Run();