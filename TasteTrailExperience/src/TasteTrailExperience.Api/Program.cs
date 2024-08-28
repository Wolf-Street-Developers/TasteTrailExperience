using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using TasteTrailData.Api.Common.Assembly;
using TasteTrailData.Api.Common.Extensions.ServiceCollection;
using TasteTrailExperience.Api.Common.Extensions.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.InitAspnetIdentity(builder.Configuration);
builder.Services.InitAuth(builder.Configuration);
builder.Services.InitSwagger();
builder.Services.InitCors();

builder.Services.RegisterBlobStorage(builder.Configuration);
builder.Services.RegisterDependencyInjection();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var assembly = Assembly.GetAssembly(typeof(ApiAssemblyMarker)) ?? throw new InvalidOperationException("Unable to load the assembly containing ApiAssemblyMarker.");

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.Run();
