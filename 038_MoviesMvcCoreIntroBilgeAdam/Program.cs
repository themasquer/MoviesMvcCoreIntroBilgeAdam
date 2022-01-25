using _038_MoviesMvcCoreIntroBilgeAdam.Contexts;
using _038_MoviesMvcCoreIntroBilgeAdam.Services;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// projemizde kullandýðýmýz abstract (soyut) base class veya interface'ler ile concrete (somut) class baðýmlýlýklarý burada yönetilir.
// unable to resolve service hatalarý öncelikle burada giderilir.
#region IoC Container : Inversion of Control Container
// IoC Container Kütüphaneleri: Autofac, Ninject
// Ekstra kaynaklar: https://autofac.org

// AddScoped(): istek(request) boyunca objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak) bir kere oluþturulur ve yanýt (response) dönene kadar bu obje hayatta kalýr.
// AddSingleton(): web uygulamasý baþladýðýnda objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak) bir kere oluþturulur ve uygulama çalýþtýðý (IIS üzerinden uygulama durudurulmadýðý veya yeniden baþlatýlmadýðý) sürece bu obje hayatta kalýr.
// AddTransient(): istek (request) baðýmsýz ihtiyaç olan objenin referansýný (genelde interface veya abstract class) kullandýðýmýz her yerde bu objeyi new'ler.

//builder.Services.AddDbContext<MoviesContext>(); // bunun yerine aþaðýdaki yazýlmalýdýr.
builder.Services.AddScoped<DbContext, MoviesContext>(); // constructor injection yapýlan yerlerde soldaki (DbContext) tipte parametreyi gördüðünde saðdaki (MoviesContext) tipte obje new'ler.

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasörü altýndaki statik olan html, css, js, imaj vb. dosyalarý

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
