using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ErrorResponse.GenerateErrorResponseModel;
});

var app = builder.Build();

app.MapControllers();

app.Run();