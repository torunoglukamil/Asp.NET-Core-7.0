using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchoolManager.Business.Services;
using SchoolManager.DataAccess.Queries;
using SchoolManager.DataAccess.Repositories;
using SchoolManager.Models.Models;
using SchoolManager.Models.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);
builder.Services.AddDbContext<school_managerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IValidator<Classroom>, ClassroomValidator>();
builder.Services.AddScoped<ClassroomQuery>();
builder.Services.AddScoped<ClassroomRepository>();
builder.Services.AddScoped<ClassroomService>();

builder.Services.AddScoped<IValidator<Student>, StudentValidator>();
builder.Services.AddScoped<StudentQuery>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<StudentService>();

var app = builder.Build();
app.MapGet("/school_manager", () => "Service is running");
app.MapControllers();
app.Run();
