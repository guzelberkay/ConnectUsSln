using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Service.Services.Abstractions;
using ConnectUs.Service.Services.Concrete;
using ConnectUs.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConnectUs.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i do�ru �ekilde yap�land�r�yoruz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45)) // Versiyon numaras�n� kendi s�r�m�n�ze g�re ayarlay�n
    ));

// AuthRepository ve AuthService'yi DI konteynerine ekliyoruz
builder.Services.AddScoped<IAuthRepository, AuthRepository>(); // IAuthRepository ve AuthRepository'yi ili�kilendiriyoruz
builder.Services.AddScoped<IAuthService, AuthService>();       // IAuthService ve AuthService'yi ili�kilendiriyoruz

// JwtTokenManager servisini DI konteynerine ekliyoruz
builder.Services.AddScoped<JwtTokenManager>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    var issuer = configuration["JwtSettings:Issuer"];
    return new JwtTokenManager(secretKey, issuer);
});

// PasswordEncoder servisini DI konteynerine ekliyoruz
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
