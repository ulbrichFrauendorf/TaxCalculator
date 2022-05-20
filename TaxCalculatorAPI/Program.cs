using Microsoft.EntityFrameworkCore;
using TaxCalculatorAPI.Authorization;
using TaxCalculatorAPI.Authorization.Middleware;
using TaxCalculatorAPI.Data;
using TaxCalculatorAPI.TaxCalculator;
using TaxCalculatorAPI.TaxCalculator.Services;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<SQLDatacontext>();
else
    builder.Services.AddDbContext<SQLDatacontext, SqliteDataContext>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });
});

//Autorization Middleware
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();

//TaxCalculator
builder.Services.AddTransient<ITaxCalculatorService, TaxCalculatorService>();
builder.Services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
builder.Services.AddTransient<ITaxSubmissionBuilder, TaxSubmissionBuilder>();
builder.Services.AddScoped<IUserService, UserService>();

#endregion Services

var app = builder.Build();

//Create Database
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SQLDatacontext>();
    dataContext.Database.Migrate();
}

#region HTTP request pipeline

app.MapControllers();
app.UseCors();
app.UseMiddleware<JwtMiddleware>();

app.Run("https://localhost:4000");

#endregion HTTP request pipeline