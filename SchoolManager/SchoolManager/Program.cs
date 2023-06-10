using Microsoft.EntityFrameworkCore;
using SchoolManager.Models.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<school_managerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/school_manager", () => "Service is running");

app.MapControllers();

app.Run();
