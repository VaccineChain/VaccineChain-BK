using Microsoft.EntityFrameworkCore;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Devices;
using vaccine_chain_bk.Repositories.Does;
using vaccine_chain_bk.Repositories.Logs;
using vaccine_chain_bk.Repositories.Statistics;
using vaccine_chain_bk.Repositories.Vaccines;
using vaccine_chain_bk.Services.Devices;
using vaccine_chain_bk.Services.Dht11;
using vaccine_chain_bk.Services.Doses;
using vaccine_chain_bk.Services.Logs;
using vaccine_chain_bk.Services.Statistics;
using vaccine_chain_bk.Services.Vaccines;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "vaccine-chain";
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "vaccinechain@123";
var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=true";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names
    }); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDoseService, DoseService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IDht11Service, Dht11Service>();
builder.Services.AddScoped<IStatisticService, StatisticService>();


// Add repositories to the container.
builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDoseRepository, DoseRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();


// Add services to the container.
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
}

app.Run();
