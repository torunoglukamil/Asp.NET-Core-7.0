using FluentValidation;
using Microsoft.EntityFrameworkCore;
using YMA.Business.Services;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;
using YMA.Entities.Entities;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;
using YMA.Entities.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ymaContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<LogRepository>();

builder.Services.AddScoped<IValidator<CreateAccountModel>, CreateAccountValidator>();
builder.Services.AddScoped<IValidator<SignInAccountModel>, SignInAccountValidator>();
builder.Services.AddScoped<IValidator<EmailModel>, EmailValidator>();
builder.Services.AddSingleton<FirebaseAuthHelper>();
builder.Services.AddScoped<IAuthQuery, FirebaseAuthQuery>();
builder.Services.AddScoped<IAuthRepository, FirebaseAuthRepository>();
builder.Services.AddScoped<AuthQuery>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IValidator<AddressModel>, AddressValidator>();

builder.Services.AddScoped<IValidator<AccountModel>, AccountValidator>();
builder.Services.AddScoped<AccountQuery>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<AdQuery>();
builder.Services.AddScoped<AdService>();

builder.Services.AddScoped<ExchangeQuery>();
builder.Services.AddScoped<ExchangeService>();

builder.Services.AddScoped<CategoryQuery>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();
app.MapGet("/yma", () => "Service is running");
app.MapControllers();
app.Run();
