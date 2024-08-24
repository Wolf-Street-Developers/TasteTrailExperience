

using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using TasteTrailData.Api.Common.Extensions.ServiceCollection;
using TasteTrailExperience.Api.Common.Extensions.ServiceCollection;
using TasteTrailExperience.Core.Feedbacks.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.InitAspnetIdentity(builder.Configuration);
builder.Services.InitAuth(builder.Configuration);
builder.Services.InitSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins(
            "http://localhost",
            "http://localhost:5137",
            "http://20.218.160.138:80",
            "http://20.218.140.188"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.RegisterBlobStorage(builder.Configuration);
builder.Services.RegisterDependencyInjection();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("LocalHostPolicy");

app.UseHttpsRedirection();

app.Run();
