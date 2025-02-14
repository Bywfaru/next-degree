using DegreePlanner.Data;
using DegreePlanner.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? 
                       builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<DegreePlannerContext>(options =>
    options
        .UseNpgsql(connectionString ??
                   throw new InvalidOperationException("Connection string not found."))
        .UseSnakeCaseNamingConvention()
);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<DegreePlannerContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapIdentityApi<ApplicationUser>();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();