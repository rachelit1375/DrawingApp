using Microsoft.EntityFrameworkCore;
using DrawingApp.Models;
using DrawingApp.Controllers;
using System;

var builder = WebApplication.CreateBuilder(args);

// ����� DbContext �� ����� ���� �������
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����� ������ Controllers
builder.Services.AddControllers();

// ����� ������ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // ����� ��������� ��� �-React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<PromptController>();

var app = builder.Build();

// ����� CORS ���� �� ��� ���
app.UseCors("AllowReactApp");

// ����� ����� �-HTTPS
app.UseHttpsRedirection();

// ����� �-Controllers (�� ������� ��-API)
app.MapControllers();

//����� ������ ��������
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    // ����� �� �� ������
//    if (!context.Drawings.Any())
//    {
//        context.Drawings.Add(new Drawing
//        {
//            Id = int.Newint(),
//            UserId = int.Newint(), // �� ID �� ����� ���� �� ��
//            Name = "���� �����",
//            Prompt = "���� �� �� ����� ������"
//        });

//        context.SaveChanges();
//    }
//}

app.Run();
