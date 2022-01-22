using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Controllers
{
    // Ekstra kaynaklar:
    // https://httpstatuses.com

    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService; // Create ve Edit işlemlerinde tüm yönetmenlere ihtiyacımız olduğu için _directorService'ı da constructor injection üzerinden kullanıyoruz.

        public MoviesController(IMovieService movieService, IDirectorService directorService)
        {
            _movieService = movieService;
            _directorService = directorService;
        }

        public IActionResult Index() // ~/Movies/Index
        {
            List<MovieModel> model = _movieService.Query().ToList();
            return View(model);
        }

        public IActionResult Details(int? id) // ~/Movies/Details/1
        {
            if (id == null)
            {
                //return new BadRequestResult();
                //return BadRequest();
                return BadRequest("Id is required!"); // 400 status code
            }

            //MovieModel model = _movieService.Query().SingleOrDefault(m => m.Id == id);
            MovieModel model = _movieService.Query().SingleOrDefault(m => m.Id == id.Value);

            if (model == null)
            {
                //return NotFound();
                return NotFound("Movie not found!"); // 404 status code
            }
            return View(model);
        }

        [HttpGet] // Create View'ı ile kullanıcının veri girişi yapabileceği formu dönüyoruz. HttpGet yazılmazsa default'tur.
        public IActionResult Create() // ~/Movies/Create
        {
            ViewBag.Directors = new MultiSelectList(_directorService.Query().ToList(), "Id", "FullName"); 
            // create view'ında bir veya daha fazla yönetmen seçimi yapabilmek için tüm yönetmen listesini ViewBag.Directors özelliğine MultiSelectList olarak atıyoruz.
            // ViewBag veya ViewData ile istenilen veriler view'a model dışında taşınabilir.
            // MVC'de aksiyonlar üzerinden view'lere sadece tek bir model dönülebilir.

            return View();
        }

        // ~/Movies/Create
        [HttpPost] // Create View'ında kullanıcının girdiği veriler Post Create aksiyonu ile form'daki giriş yapılan HTML elemanlarının name özellikleri parametre olarak aksiyona tanımlanarak alınır ve kullanılır.
        //public IActionResult Create(string? Name, short? ProductionYear, string? BoxOfficeReturnModel, List<int>? DirectorIds)
        public IActionResult Create(MovieModel model) // yukarıdaki parametre olarak kullanılan özellikler MovieModel içerisinde olduğu için MovieModel tipinde parametre kullanılabilir.
        {
            // MVC'de model'deki data annotation'lara göre validasyon ModelState.IsValid ile yapılır.
            // View'de validation summary veya validation message HTML helper'ları ile validasyon hataları gösterilebilir.
            // .NET'te validasyon hata mesajları dillere göre otomatik tanımlanmıştır.
            if (ModelState.IsValid) 
            {
                ResultStatus result = _movieService.Add(model);
                if (result == ResultStatus.Success)
                {

                    // Film ekleme işleminden sonra tekrar film listeleme aksiyonunu çağırıyoruz.
                    TempData["Message"] = "Movie created successfully."; // Index view'ında TempData["Message"] dolu mu diye kontrol edip doluysa kullanmamız gerekir.
                    // Başka bir aksiyona yönlendirme yapılıyorsa TempData, aksiyonun view'ı dönülüyorsa ViewData veya ViewBag kullanılmalıdır.

                    //return RedirectToAction("Index");
                    return RedirectToAction(nameof(Index)); // yukarıdaki gibi Index'i string olarak yazmak yerine hataları engellemek için Index methodunun adını nameof ile kullanabiliyoruz.

                }
                if (result == ResultStatus.Exception)
                {
                    //return View("MyError", "An error occurred."); // MyError view'ında model null ise "An error occurred." yazdırdığımızdan model parametresi olarak göndermemize gerek yoktur.
                    return View("MyError");
                }
                if (result == ResultStatus.StringToDoubleConversionFailed)
                {
                    ViewBag.ErrorMessage = "Box office return must be numeric!"; // Create view'ında ViewBag.ErrorMessage dolu mu diye kontrol edip doluysa kullanmamız gerekir.
                }
                if (result == ResultStatus.EntityExists)
                {
                    ViewBag.ErrorMessage = "Movie with the same name exists!";
                }
            }

            // eğer validasyondan geçemezse tüm yönetmenleri tekrar doldurup kullanıcının girmiş olduğu verileri model üzerinden view'a gönderiyoruz.
            ViewBag.Directors = new MultiSelectList(_directorService.Query().ToList(), "Id", "FullName", model.DirectorIds);
            return View(model);
        }

        // Scaffolding şablonları C:\Users\{WindowsKullanıcısı}\.nuget\packages\microsoft.visualstudio.web.codegenerators.mvc\6.0.1\Templates\ViewGenerator
        // klasörü altındaki dosyalar üzerinden değiştirilebilir.
        public IActionResult Edit(int? id) // ~/Movies/Edit/1
        {
            if (id == null)
            {
                //return BadRequest("Id is required!"); // 400 status code
                return View("MyError", "Id is required!");
            }
            MovieModel model = _movieService.Query().SingleOrDefault(m => m.Id == id.Value);
            if (model == null)
            {
                //return NotFound("Movie not found!"); // 404 status code
                return View("MyError", "Movie not found!");
            }
            ViewBag.Directors = new MultiSelectList(_directorService.Query().ToList(), "Id", "FullName", model.DirectorIds);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MovieModel model) // ~/Movies/Edit
        {
            if (ModelState.IsValid)
            {
                ResultStatus result = _movieService.Update(model);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Movie updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                if (result == ResultStatus.Exception)
                {
                    return View("MyError");
                }
                if (result == ResultStatus.StringToDoubleConversionFailed)
                {
                    ModelState.AddModelError("", "Box office return must be numeric!");
                }
                if (result == ResultStatus.EntityExists)
                {
                    ModelState.AddModelError("", "Movie with the same name exists!");
                }
            }
            ViewBag.Directors = new MultiSelectList(_directorService.Query().ToList(), "Id", "FullName", model.DirectorIds);
            return View(model);
        }

        // Genelde kullanıcı konfirmasyonu olmadan burada olduğu gibi silme işlemi yapılmaz.
        public IActionResult Delete(int? id) // ~/Movies/Delete/1
        {
            if (id == null)
                return View("MyError", "Id is required!");
            ResultStatus result = _movieService.Delete(id.Value);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Movie deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (result == ResultStatus.RelationalEntitiesExist)
            {
                TempData["Message"] = "Movie cannot be deleted because it has reviews!";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); // exception result status
        }
    }
}
