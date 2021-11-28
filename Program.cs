using FlasherWebApi.Models;
using FlasherWebApi.Options;
using FlasherWebApi.Services;
using FlasherWebApi.Services.Imp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "flasher api",
        Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme{Reference = new OpenApiReference{
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }},new string[] {}}});
    });


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:3000/"));
});

builder.Services.AddDbContext<DatabaseContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("flasherDb")));
//builder.Services.AddDbContext<DatabaseContext>(
//       options => options.("Server=.;Database=flasher;Trusted_Connection=True;"));

// For Identity
builder.Services.AddIdentity<User, Role>()
.AddEntityFrameworkStores<DatabaseContext>()
.AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JWT")["Audience"],
        ValidIssuer = builder.Configuration.GetSection("JWT")["Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["SecretKey"]))
    };
});


builder.Services.Configure<PushNotificationServiceOptions>(builder.Configuration.GetSection("VAPID"));

builder.Services.AddTransient<IPushNotificationService, PushNotificationService>();
builder.Services.AddTransient<IPushSubscriptionService, PushSubscriptionService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
