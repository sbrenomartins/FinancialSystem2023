using Api.Token;
using Domain.Interfaces.Categories;
using Domain.Interfaces.Expenses;
using Domain.Interfaces.FinancialSystems;
using Domain.Interfaces.FinancialSystemUsers;
using Domain.Interfaces.Generic;
using Domain.Interfaces.Services;
using Domain.Services;
using Entities.Entities;
using Infrastructure;
using Infrastructure.Configurations;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "FinancialSystem2023", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BaseContext>();

// Interfaces and repositories
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<ICategory, CategoryRepository>();
builder.Services.AddSingleton<IExpense, ExpenseRepository>();
builder.Services.AddSingleton<IFinancialSystem, FinancialSystemRepository>();
builder.Services.AddSingleton<IFinancialSystemUser, FinancialSystemUserRepository>();

// Services
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IExpenseService, ExpenseService>();
builder.Services.AddSingleton<IFinancialSystemService, FinancialSystemService>();
builder.Services.AddSingleton<IFinancialSystemUserService, FinancialSystemUserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Test.Security.Bearer",
            ValidAudience = "Test.Security.Bearer",
            IssuerSigningKey = JwtSecurityKey.Create("Secret_Key_12345678")
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
