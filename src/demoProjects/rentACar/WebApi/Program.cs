using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Persistance;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ApplicationExtensionsIoC();
builder.Services.PersistanceExtensionsIoC(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment()) // dedimiz 

app.ConfigureCustomExceptionMiddleware();   // Cross Cutting Concerns CorePackage den geliyor 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
