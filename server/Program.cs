using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// הוספת DbContext עם חיבור למסד הנתונים
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// הוספת שירותי Controllers
builder.Services.AddControllers();

// הוספת שירותי CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // כתובת האפליקציה שלך ב-React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// הפעלת CORS לפני כל דבר אחר
app.UseCors("AllowReactApp");

// הפעלת ניתוב ל-HTTPS
app.UseHttpsRedirection();

// אם אין לך Authentication/Authorization, אפשר לוותר על השורה הזו
// app.UseAuthorization();

// מיפוי ה-Controllers (כל הפעולות מה-API)
app.MapControllers();

app.Run();
