using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Models;
using WebApi.Data;
using WebApi.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();
builder.Services.AddJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
