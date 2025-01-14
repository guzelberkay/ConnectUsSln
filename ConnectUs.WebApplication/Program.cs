using ConnectUs.Core.Utilities;
using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Data.Repositories.Concretes;
using ConnectUs.Service.Services.Abstractions;
using ConnectUs.Service.Services.Concrete;
using ConnectUs.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// T�rk�e k�lt�r ayarlar�
var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// T�rk�e k�lt�r i�in RequestLocalization yap�land�rmas�
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
};

// Loglama yap�land�rmas�
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Konsola log yazd�r�r
builder.Logging.AddDebug();   // Debug ��kt�s�na log yazar

// appsettings.json'u y�kleme
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// EmailService i�in gerekli servisi ekliyoruz
builder.Services.AddScoped<IEmailService, EmailService>();

// DbContext yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// JwtSettings'i appsettings.json'dan okuyup DI konteynerine ekle
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Servisleri DI konteynerine ekliyoruz
builder.Services.AddScoped<JwtTokenManager>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();

builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IOurServicesService, OurServicesService>();
builder.Services.AddScoped<IOurServicesRepository, OurServicesRepository>();

builder.Services.AddScoped<PasswordEncoder>();

// DefaultUserSeederService servisini Transient olarak DI konteynerine ekliyoruz
builder.Services.AddTransient<IHostedService, DefaultUserSeederService>();

// Swagger'� etkinle�tiriyoruz
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// CORS yap�land�rmas�
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://www.isttekzemin.com") // �zin verilen origin
              .AllowAnyHeader()                           // Herhangi bir ba�l�k
              .AllowAnyMethod();                          // Herhangi bir HTTP metodu
    });
});

var app = builder.Build();

// Swagger'� kullanmaya ba�l�yoruz
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS y�nlendirmesi
app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS'u etkinle�tir
app.UseCors("AllowSpecificOrigin");

// T�rk�e k�lt�r� etkinle�tir
app.UseRequestLocalization(localizationOptions);

app.UseRouting();
app.UseAuthorization();

// Varsay�lan route yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
