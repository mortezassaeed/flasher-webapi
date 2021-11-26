using FlasherWebApi.Models;
using FlasherWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:3000/"));
});
builder.Services.AddDbContext<DatabaseContext>(
       options => options.UseSqlServer("Server=.;Database=flasher;Trusted_Connection=True;"));


builder.Services.Configure<PushNotificationServiceOptions>(builder.Configuration.GetSection("VAPID"));

builder.Services.AddSingleton<IPushNotificationService, WebPushPushNotificationService>();

var app = builder.Build();

app.UseCors(
       options => options.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader()
   );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
