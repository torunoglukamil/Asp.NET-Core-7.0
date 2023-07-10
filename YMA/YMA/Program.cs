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
builder.Services.AddScoped<ResponseHelper>();

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

builder.Services.AddScoped<BrandQuery>();
builder.Services.AddScoped<BrandService>();

builder.Services.AddScoped<CompanyQuery>();
builder.Services.AddScoped<CompanyService>();

builder.Services.AddScoped<ProductQuery>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<IValidator<ReplyCompanyInviteModel>, ReplyCompanyInviteValidator>();
builder.Services.AddScoped<IValidator<CompanyInviteModel>, CompanyInviteValidator>();
builder.Services.AddScoped<CompanyInviteQuery>();
builder.Services.AddScoped<CompanyInviteRepository>();
builder.Services.AddScoped<CompanyInviteService>();

builder.Services.AddScoped<VersionQuery>();
builder.Services.AddScoped<VersionService>();

var app = builder.Build();
app.MapGet("/yma", () => "Service is running");
app.MapControllers();
app.Run();
