using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using vaccine_chain_bk.Hubs;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Devices;
using vaccine_chain_bk.Repositories.Does;
using vaccine_chain_bk.Repositories.Logs;
using vaccine_chain_bk.Repositories.Roles;
using vaccine_chain_bk.Repositories.Statistics;
using vaccine_chain_bk.Repositories.Users;
using vaccine_chain_bk.Repositories.Vaccines;
using vaccine_chain_bk.Services;
using vaccine_chain_bk.Services.Devices;
using vaccine_chain_bk.Services.Dht11;
using vaccine_chain_bk.Services.Doses;
using vaccine_chain_bk.Services.Logs;
using vaccine_chain_bk.Services.Statistics;
using vaccine_chain_bk.Services.Users;
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
builder.Services.AddScoped<IUserService, UserService>();

// Add repositories to the container.
builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDoseRepository, DoseRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// 
builder.Services.AddHttpClient<HttpClientService>();


// Add services to the container.
builder.Services.AddCors();

// Thêm chính sách CORS với các nguồn được chỉ định
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("http://localhost:4200") // Chỉ định nguồn cụ thể (Angular app)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Cho phép gửi cookie/credentials nếu cần
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Vaccine API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Thêm SignalR
builder.Services.AddSignalR();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();


// Sử dụng CORS
app.UseCors("AllowSpecificOrigins");

// Map SignalR Hub
app.MapHub<TemperatureHub>("/temperatureHub");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
}

app.Run();
