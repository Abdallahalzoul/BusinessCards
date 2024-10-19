using Microsoft.Extensions.Configuration;
using System.Data;
using Services;
using AutoMapper;
using Domain.AutoMapper;
using Microsoft.AspNetCore.Http.Features;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddInfrastructure(builder.Configuration);
IMapper mapper = AutoMapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 

app.UseCors(x =>
{
    x.AllowAnyHeader().AllowAnyMethod();
    x.AllowAnyOrigin().AllowAnyMethod();
});


app.UseAuthorization();

app.MapControllers();

app.Run();
