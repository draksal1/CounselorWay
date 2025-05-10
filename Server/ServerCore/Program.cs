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

// ���������� �������� � ��������� DI
builder.Services.AddControllers();

// ����������� ������������
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IChallengeMapRepository, InMemoryChallengeMapRepository>();
builder.Services.AddSingleton<ICampSeasonRepository, InMemoryCampSeasonRepository>();

// ����������� ��������
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<ICampSeasonService, CampSeasonService>();

// ��������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ������������ HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ������� ������������
app.MapControllers();

app.Run();
