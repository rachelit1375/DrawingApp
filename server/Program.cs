using Microsoft.EntityFrameworkCore;
using DrawingApp.Models;
using DrawingApp.Controllers;
using System;

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

builder.Services.AddHttpClient<PromptController>();

var app = builder.Build();

// הפעלת CORS לפני כל דבר אחר
app.UseCors("AllowReactApp");

// הפעלת ניתוב ל-HTTPS
app.UseHttpsRedirection();

// מיפוי ה-Controllers (כל הפעולות מה-API)
app.MapControllers();

//יצירת נתונים התחלתיים
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // בדיקה אם יש נתונים
//    if (!context.Drawings.Any())
//    {
//        context.Drawings.Add(new Drawing
//        {
//            Id = int.Newint(),
//            UserId = int.Newint(), // או ID של משתמש קיים אם יש
//            Name = "ציור בדיקה",
//            Prompt = "צייר עץ עם שמיים כחולים"
//        });

//        context.SaveChanges();
//    }
//}

app.Run();
