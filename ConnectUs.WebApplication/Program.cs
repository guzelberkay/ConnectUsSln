using ConnectUs.Core.Utilities;
using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Service.Services.Abstractions;
using ConnectUs.Service.Services.Concrete;
using ConnectUs.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// DbContext yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45))
    ));

// JwtSettings'i appsettings.json'dan okuyup DI konteynerine ekle
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Servisleri DI konteynerine ekliyoruz
builder.Services.AddScoped<JwtTokenManager>(); // JwtTokenManager'� IOptions ile kullanacak �ekilde kaydediyoruz
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<PasswordEncoder>();

// DefaultUserSeederService servisini Transient olarak DI konteynerine ekliyoruz
builder.Services.AddTransient<IHostedService, DefaultUserSeederService>();

// Swagger'� etkinle�tiriyoruz
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Swagger'� kullanmaya ba�l�yoruz
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Swagger'� aktif hale getiriyoruz
    app.UseSwaggerUI(); // Swagger UI'yi aktif hale getiriyoruz
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
