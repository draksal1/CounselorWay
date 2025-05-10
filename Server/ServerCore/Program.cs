using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerCore.Controllers;
using ServerCore.Models;
using ServerCore.Repositories;
using ServerCore.Repositories.Contracts;
using ServerCore.Repositories.Implementations;
using ServerCore.Services;
using ServerCore.Services.Contracts;
using ServerCore.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер DI
builder.Services.AddControllers();

// Регистрация репозиториев
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IChallengeMapRepository, InMemoryChallengeMapRepository>();
builder.Services.AddSingleton<ICampSeasonRepository, InMemoryCampSeasonRepository>();

// Регистрация сервисов
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<ICampSeasonService, CampSeasonService>();

// Настройка Swagger для тестирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Конфигурация HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Маппинг контроллеров
app.MapControllers();

app.Run();
