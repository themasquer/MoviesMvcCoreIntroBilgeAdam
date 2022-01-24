#nullable disable
using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Contexts;
using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Controllers
{
    public class DirectorsController : Controller
    {
        // DbContext üzerinden değil servisler üzerinden aksiyonları yazacağız.
        private readonly MoviesContext _context;

        //public DirectorsController(MoviesContext context)
        //{
        //    _context = context;
        //}

        private readonly IDirectorService _directorService;
        private readonly IMovieService _movieService;

        public DirectorsController(IDirectorService directorService, IMovieService movieService)
        {
            _directorService = directorService;
            _movieService = movieService;
        }

        // GET: Directors
        //public async Task<IActionResult> Index() 
        //{
        //    return View(await _context.Directors.ToListAsync());
        //}
        // burada asenkron methodlar kullanmamıza şimdilik gerek yok.
        public IActionResult Index()
        {
            return View(_directorService.Query().ToList());
        }

        // GET: Directors/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var director = await _context.Directors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (director == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(director);
        //}
        // burada asenkron methodlar kullanmamıza şimdilik gerek yok.
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id is required!");
            var model = _directorService.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Director not found!");
            return View(model);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            List<SelectListItem> movieSelectListItems = _movieService.Query().Select(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
            ViewData["Movies"] = new MultiSelectList(movieSelectListItems, "Value", "Text");

            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Surname,Retired")] Director director)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(director);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(director);
        //}
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Add(director);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Director created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                if (result == ResultStatus.Exception)
                {
                    return View("MyError");
                }
                if (result == ResultStatus.EntityExists)
                {
                    ModelState.AddModelError("", "Director with the same name and surname exists!");
                }
            }
            List<SelectListItem> movieSelectListItems = _movieService.Query().Select(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
            ViewData["Movies"] = new MultiSelectList(movieSelectListItems, "Value", "Text", director.MovieIds);
            return View(director);
        }

        // GET: Directors/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var director = await _context.Directors.FindAsync(id);
        //    if (director == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(director);
        //}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("MyError", "Id is required!");
            }
            var model = _directorService.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
            {
                return View("MyError", "Director not found!");
            }
            ViewBag.Movies = new MultiSelectList(_movieService.Query().ToList(), "Id", "Name", model.MovieIds);
            return View(model);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Retired")] Director director)
        //{
        //    if (id != director.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(director);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DirectorExists(director.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(director);
        //}
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Update(director);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Director updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                if (result == ResultStatus.Exception)
                {
                    return View("MyError");
                }
                if (result == ResultStatus.EntityExists)
                {
                    ModelState.AddModelError("", "Director with the same name and surname exists!");
                }
            }
            ViewBag.Movies = new MultiSelectList(_movieService.Query().ToList(), "Id", "Name", director.MovieIds);
            return View(director);
        }

        // GET: Directors/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var director = await _context.Directors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (director == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(director);
        //}
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id is required!");
            var model = _directorService.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Director not found!");
            return View(model);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")] // Aksiyon adı ne olursa olsun (DeleteConfirmed) eğer ActionName kullanılırsa ActionName olarak tanımlanan (Delete) üzerinden aksiyon çağrılır.
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var director = await _context.Directors.FindAsync(id);
        //    _context.Directors.Remove(director);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _directorService.Delete(id);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Director deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); // exception result status
        }

        //private bool DirectorExists(int id)
        //{
        //    return _context.Directors.Any(e => e.Id == id);
        //}
    }
}
