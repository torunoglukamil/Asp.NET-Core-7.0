using MyTestApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<FileRepository>();

var app = builder.Build();
app.MapGet("/my_test_api", () => "My Test API service is running...");
app.MapControllers();
app.Run();
