using DataAccessLayer;
using BusinessLogicLayer;
using FluentValidation.AspNetCore;
using OrdersMicroservice.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessLogicLayer(builder.Configuration);

builder.Services.AddDataAccessLayer(builder.Configuration);

// Add controllers
builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

/*
builder.Services.AddCors(options =>
{
    // Loose policy for development
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins("https://localhost:7071", "http://localhost:5181")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    // Strict policy for production
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://localhost:7071", "http://localhost:5181")
              .WithMethods("GET", "POST")
              .WithHeaders("Content-Type", "Authorization")
              .AllowCredentials();
    });
});
*/

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
