using System.Text.Json;
using System.Text.Json.Serialization;
using BankingControlPanel.Api.Middlewares;
using BankingControlPanel.Application.Interfaces.Repositories;
using BankingControlPanel.Application.Mapper;
using BankingControlPanel.Application.Services;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Infrastructure.Data;
using BankingControlPanel.Infrastructure.Repositories;
using BankingControlPanel.Infrastructure.Services;
using BankingControlPanel.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddMemoryCache(); // Add the memory cache service
builder.Services.AddScoped<ICacheService, MemoryCacheService>();
builder.Services.AddScoped<ISearchCriteriaRepository, SearchCriteriaRepository>(); 
builder.Services.AddScoped<ISearchCriteriaService,SearchCriteriaService>(); 




// Configure controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON to serialize enums as strings using camel case
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankingControlPanel v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();