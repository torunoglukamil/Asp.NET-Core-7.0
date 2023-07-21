using Microsoft.EntityFrameworkCore;
using YMA.Business.Services;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Providers;
using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;
using YMA.Entities.Entities;
using YMA.Entities.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ymaContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<FirebaseAuthenticationProvider>();
builder.Services.AddScoped<LogRepository>();
builder.Services.AddScoped<ResponseHelper>();

builder.Services.AddScoped<IAuthQuery, FirebaseAuthQuery>();
builder.Services.AddScoped<IAuthRepository, FirebaseAuthRepository>();
builder.Services.AddScoped<AuthQuery>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<AccountQuery>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<CompanyQuery>();
builder.Services.AddScoped<CompanyService>();

builder.Services.AddScoped<CompanyInviteQuery>();
builder.Services.AddScoped<CompanyInviteRepository>();
builder.Services.AddScoped<CompanyInviteService>();

builder.Services.AddScoped<CategoryQuery>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddScoped<BrandQuery>();
builder.Services.AddScoped<BrandService>();

builder.Services.AddScoped<ProductQuery>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<FavoriteProductQuery>();
builder.Services.AddScoped<FavoriteProductRepository>();
builder.Services.AddScoped<FavoriteProductService>();

builder.Services.AddScoped<AdQuery>();
builder.Services.AddScoped<AdService>();

builder.Services.AddScoped<ExchangeQuery>();
builder.Services.AddScoped<ExchangeService>();

builder.Services.AddScoped<VersionQuery>();
builder.Services.AddScoped<VersionService>();

var app = builder.Build();
app.MapGet("/yma", () => "Service is running");
app.MapControllers();
app.Run();
