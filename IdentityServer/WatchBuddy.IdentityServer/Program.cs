using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchBuddy.IdentityServer.Models;

var builder = WebApplication.CreateBuilder(args);


// Veritabanı Bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Identity yapılandırmasını ekleyin
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); // Token sağlayıcıları eklemek (şifre sıfırlama vb. işlemler için)


// OpenIddict Konfigürasyonu
builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<ApplicationDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("/connect/authorize")
            .SetTokenEndpointUris("/connect/token")
            .AllowPasswordFlow()
            .AllowRefreshTokenFlow()
            .SetAccessTokenLifetime(TimeSpan.FromMinutes(60))
            .SetRefreshTokenLifetime(TimeSpan.FromDays(30));

        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Veritabanı migrasyonu ve geliştirme ortamı
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    if (!userManager.Users.Any())
    {
       userManager.CreateAsync(new ApplicationUser{UserName = "sesenturk", Email = "sesenturk@gmail.com"}, "Semih123!!").Wait();
    }
}


app.MapControllers();

app.Run();