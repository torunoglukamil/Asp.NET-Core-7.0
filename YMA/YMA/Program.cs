using FluentValidation;
using Microsoft.EntityFrameworkCore;
using YMA.Business.Interfaces;
using YMA.Business.Services;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;
using YMA.Models.Models;
using YMA.Models.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ymaContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ResponseHelper>();

builder.Services.AddScoped<IValidator<AuthModel>, AuthValidator>();
builder.Services.AddScoped<IAuthService, FirebaseAuthService>();

builder.Services.AddScoped<IValidator<address>, AddressValidator>();

builder.Services.AddScoped<IValidator<account>, AccountValidator>();
builder.Services.AddScoped<AccountQuery>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

var app = builder.Build();
app.MapGet("/yma", () => "Service is running");
app.MapControllers();
app.Run();
