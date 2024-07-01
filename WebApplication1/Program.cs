using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Helpers;
using WebApplication1.interfaces;
using WebApplication1.middlewares;
using WebApplication1.Models;
using WebApplication1.Models.common;
using WebApplication1.repository;
using WebApplication1.utils;


var builder = WebApplication.CreateBuilder(args);


// ------------------------------------------------ AUTH START -----------------------------------------


builder.Services.AddAuthentication("Custom")
    .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("Custom", null);

builder.Services.AddAuthorization();

// ------------------------------------------------ AUTH END -------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ApiResponse<dynamic>.GenerateErrorResponseModel;
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddSingleton<JwtHelper>();


var app = builder.Build();
app.UseAuthentication();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();