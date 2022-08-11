using AspNetCoreRateLimit;
using ESourcing.ApiGateway.Infrastructure;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var host = builder.Host;

host.ConfigureAppConfiguration((context, config) => { config.AddJsonFile("ocelot.json"); });

services.AddpProjectDependencies();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


var app = builder.Build();

IIpPolicyStore ipPolicy = app.Services.GetRequiredService<IIpPolicyStore>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();

await app.UseOcelot();

app.UseAuthorization();

app.MapControllers();

await ipPolicy.SeedAsync();
await app.RunAsync();
