using MediatR;
using Microsoft.AspNetCore.Hosting;
using Proji.Application.Commands;
using Proji.Application.Services;
using Proji.Domain.Registration;
using Proji.Domain.Services;
using Proji.Domain.Validation;
using Proji.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
});

builder.Services.AddScoped<IFeeCalculationService, FeeCalculationService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddApplicationServices();

// Register all command and query handlers using reflection
// Register MediatR handlers
builder.Services.AddMediatRHandlers(typeof(AddVehicleCommand).Assembly); // Adjust assembly as needed


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });

    // CORS policy for development environment
    app.UseCors(options =>
    {
        options.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Use routing
app.UseRouting();

// Use authentication and authorization if needed
app.UseAuthorization();

// Use endpoints
app.MapControllers();

app.Run();
