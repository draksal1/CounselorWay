var builder = WebApplication.CreateBuilder(args);

// ��������� ������ API-����������� (��� Views)
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

// �������� ���������� �������������
app.MapControllers();

var url = app.Configuration["Server:Url"] ?? "http://localhost:5000";
app.Run(url);