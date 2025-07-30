var builder = WebApplication.CreateBuilder(args);

// ����� ������ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // ����� �-React �������
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ����� ������ Controllers
builder.Services.AddControllers();

var app = builder.Build();

// ����� CORS ���� Authorization
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

