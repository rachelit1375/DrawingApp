var builder = WebApplication.CreateBuilder(args);

// הוספת שירותי CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // כתובת ה-React המקומית
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// הוספת שירותי Controllers
builder.Services.AddControllers();

var app = builder.Build();

// הפעלת CORS לפני Authorization
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

