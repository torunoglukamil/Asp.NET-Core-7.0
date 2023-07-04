using FluentValidation;
using Microsoft.EntityFrameworkCore;
using YMA.Business.Interfaces;
using YMA.Business.Services;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;
using YMA.Entities.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ymaContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<LogConverter>();
builder.Services.AddScoped<LogRepository>();

builder.Services.AddSingleton<ResponseHelper>();

builder.Services.AddScoped<IValidator<CreateAccountModel>, CreateAccountValidator>();
builder.Services.AddScoped<IValidator<SignInAccountModel>, SignInAccountValidator>();
builder.Services.AddScoped<IValidator<EmailModel>, EmailValidator>();
builder.Services.AddScoped<IAuthService, FirebaseAuthService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IValidator<AddressModel>, AddressValidator>();

builder.Services.AddSingleton<AccountConverter>();
builder.Services.AddScoped<IValidator<AccountModel>, AccountValidator>();
builder.Services.AddScoped<AccountQuery>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddSingleton<AdConverter>();
builder.Services.AddScoped<AdQuery>();
builder.Services.AddScoped<AdService>();

builder.Services.AddSingleton<ExchangeConverter>();
builder.Services.AddScoped<ExchangeQuery>();
builder.Services.AddScoped<ExchangeService>();

builder.Services.AddSingleton<CategoryConverter>();
builder.Services.AddScoped<CategoryQuery>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();
app.MapGet("/yma", () => "Service is running");
app.MapControllers();
app.Run();
