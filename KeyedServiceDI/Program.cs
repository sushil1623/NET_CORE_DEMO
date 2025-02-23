using KeyedServiceDI;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddKeyedScoped<INotificationService,EmailNotificationService>("Email");
builder.Services.AddKeyedScoped<INotificationService, SMSNotificationService>("SMS");
builder.Services.AddKeyedScoped<INotificationService, PushNotificationServices>("PushNotification");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
