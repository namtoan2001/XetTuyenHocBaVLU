using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.Services;
using XetTuyenVLU.SettingsForMail;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<XetTuyenVLUContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("XetTuyenVLUContextConnection")));

// Add Authentication & Authorization

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Admin"));
//    options.AddPolicy("AdminOrManagerOnly", policy => policy.RequireAssertion(context => context.User.HasClaim("role", "Admin") || context.User.HasClaim("role", "Manager")));
//});

//Add DI
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IAdmissionService, AdmissionService>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IPhaseService, PhaseService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IScheduleService, ScheduleService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAdmisionAdminService, AdmissionAdminService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options
                .WithOrigins("https://localhost:44348", "http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = Text.Plain;
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        await context.Response.WriteAsync(exceptionHandlerPathFeature?.Error.Message);
    });
});

app.UseHsts();

app.MapControllers();

app.Run();
