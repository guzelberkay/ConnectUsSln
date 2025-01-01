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

// appsettings.json'u yükleme
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// EmailService için gerekli servisi ekliyoruz
builder.Services.AddScoped<IEmailService, EmailService>();

// DbContext yapýlandýrmasý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 45))
    ));

// JwtSettings'i appsettings.json'dan okuyup DI konteynerine ekle
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Servisleri DI konteynerine ekliyoruz
builder.Services.AddScoped<JwtTokenManager>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<PasswordEncoder>();

// DefaultUserSeederService servisini Transient olarak DI konteynerine ekliyoruz
builder.Services.AddTransient<IHostedService, DefaultUserSeederService>();

// Swagger'ý etkinleþtiriyoruz
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Swagger'ý kullanmaya baþlýyoruz
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
