using Microsoft.EntityFrameworkCore;
using OrderManagement.API.ExceptionHendlers;
using OrderManagement.Infrastructure.Persistence;
using OrderManagement.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddDIServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderManagementContext>();
        dbContext.Database.Migrate(); 
    }
}


app.UseCors("AllowAll");

app.UseExceptionHandler();

app.MapControllers();


app.Run();
