var builder = WebApplication.CreateBuilder(args);

// Добавляем только API-контроллеры (без Views)
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// Включаем атрибутную маршрутизацию
app.MapControllers();

var url = app.Configuration["Server:Url"] ?? "http://localhost:5000";
app.Run(url);