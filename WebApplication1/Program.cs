using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.interfaces;
using WebApplication1.middlewares;
using WebApplication1.Models;
using WebApplication1.Models.common;
using WebApplication1.repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ApiResponse<dynamic>.GenerateErrorResponseModel;
});
builder.Services.AddDbContext<ItemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();