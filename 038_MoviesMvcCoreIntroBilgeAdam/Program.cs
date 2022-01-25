using _038_MoviesMvcCoreIntroBilgeAdam.Contexts;
using _038_MoviesMvcCoreIntroBilgeAdam.Services;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// projemizde kulland���m�z abstract (soyut) base class veya interface'ler ile concrete (somut) class ba��ml�l�klar� burada y�netilir.
// unable to resolve service hatalar� �ncelikle burada giderilir.
#region IoC Container : Inversion of Control Container
// IoC Container K�t�phaneleri: Autofac, Ninject
// Ekstra kaynaklar: https://autofac.org

// AddScoped(): istek(request) boyunca objenin referans�n� (genelde interface veya abstract class) kulland���m�z yerde obje (somut class'tan olu�turulacak) bir kere olu�turulur ve yan�t (response) d�nene kadar bu obje hayatta kal�r.
// AddSingleton(): web uygulamas� ba�lad���nda objenin referans�n� (genelde interface veya abstract class) kulland���m�z yerde obje (somut class'tan olu�turulacak) bir kere olu�turulur ve uygulama �al��t��� (IIS �zerinden uygulama durudurulmad��� veya yeniden ba�lat�lmad���) s�rece bu obje hayatta kal�r.
// AddTransient(): istek (request) ba��ms�z ihtiya� olan objenin referans�n� (genelde interface veya abstract class) kulland���m�z her yerde bu objeyi new'ler.

//builder.Services.AddDbContext<MoviesContext>(); // bunun yerine a�a��daki yaz�lmal�d�r.
builder.Services.AddScoped<DbContext, MoviesContext>(); // constructor injection yap�lan yerlerde soldaki (DbContext) tipte parametreyi g�rd���nde sa�daki (MoviesContext) tipte obje new'ler.

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
app.UseStaticFiles(); // wwwroot klas�r� alt�ndaki statik olan html, css, js, imaj vb. dosyalar�

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
