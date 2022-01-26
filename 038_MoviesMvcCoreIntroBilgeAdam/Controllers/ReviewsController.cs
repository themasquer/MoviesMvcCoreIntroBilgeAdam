#nullable disable
using _038_MoviesMvcCoreIntroBilgeAdam.Contexts;
using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MoviesContext _context;

        //public ReviewsController(MoviesContext context)
        //{
        //    _context = context;
        //}

        private readonly IReviewService _reviewService;
        private readonly IMovieService _movieService;

        public ReviewsController(IReviewService reviewService, IMovieService movieService)
        {
            _reviewService = reviewService;
            _movieService = movieService;
        }

        // GET: Reviews
        //public async Task<IActionResult> Index()
        //{
        //    var moviesContext = _context.Reviews.Include(r => r.Movie);
        //    return View(await moviesContext.ToListAsync());
        //}
        public IActionResult Index()
        {
            return View(_reviewService.Query().ToList());
        }

        // GET: Reviews/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Reviews
        //        .Include(r => r.Movie)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(review);
        //}
        public IActionResult Details(int? id)
        {
            if (id == null)
                return View("MyError", "Id is required!");
            var model = _reviewService.Query().SingleOrDefault(r => r.Id == id.Value);
            if (model == null)
                return View("MyError", "Review not found!");
            return View(model);
        }

        // GET: Reviews/Create
        //public IActionResult Create()
        //{
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name");
        //    return View();
        //}
        public IActionResult Create()
        {
            // create view'ında film seçimi yapabilmek için tüm film listesini ViewBag.Movies özelliğine SelectList olarak atıyoruz.
            ViewBag.Movies = new SelectList(_movieService.Query().ToList(), "Id", "Name");

            // yeni boş bir model oluşturup form alanlarının ilk değerlerini set ederek View'a gönderebiliriz:
            var model = new ReviewModel()
            {
                Id = 0,
                DateModel = DateTime.Today.ToString("MM/dd/yyyy", new CultureInfo("en-US")),
                Rating = 5,
                RatingCssClassModel = "badge bg-warning text-dark"
            };

            return View(model);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Content,Rating,Reviewer,Date,MovieId")] Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(review);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", review.MovieId);
        //    return View(review);
        //}
        public IActionResult Create(ReviewModel review)
        {
            if (ModelState.IsValid)
            {
                var result = _reviewService.Add(review);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Review created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                return View("MyError"); // exception status result
            }
            ViewBag.Movies = new SelectList(_movieService.Query().ToList(), "Id", "Name", review.MovieId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", review.MovieId);
        //    return View(review);
        //}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View("MyError", "Id is required!");
            var model = _reviewService.Query().SingleOrDefault(d => d.Id == id.Value);
            if (model == null)
                return View("MyError", "Review not found!");
            ViewBag.Movies = new SelectList(_movieService.Query().ToList(), "Id", "Name", model.MovieId);
            return View(model);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Rating,Reviewer,Date,MovieId")] Review review)
        //{
        //    if (id != review.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(review);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReviewExists(review.Id))
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
        //    ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", review.MovieId);
        //    return View(review);
        //}
        public IActionResult Edit(ReviewModel review)
        {
            if (ModelState.IsValid)
            {
                var result = _reviewService.Update(review);
                if (result == ResultStatus.Success)
                {
                    TempData["Message"] = "Review updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                return View("MyError"); // exception status result
            }
            ViewBag.Movies = new SelectList(_movieService.Query().ToList(), "Id", "Name", review.MovieId);
            return View(review);
        }

        // Alertify js-css kütüphanesi ile kaydı silmek isteyip istemediğimizi sorduğumuzdan bu action'a artık gerek yok.
        // GET: Reviews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Reviews
        //        .Include(r => r.Movie)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(review);
        //}

        // Alertify js-css kütüphanesi ile kaydı silmek isteyip istemediğimizi sorduğumuzdan bu action'a artık gerek yok.
        // POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var review = await _context.Reviews.FindAsync(id);
        //    _context.Reviews.Remove(review);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        /* 
           Index view'ında kullandığımız Alertify.js js-css kütüphanesini projenin wwwroot/lib klasörünü seçip Add -> Client-Side Library
           diyerek gelen ekrandan AlertifyJS aratıp sonuçlardan AlertifyJS@1.13.1 seçtikten sonra aşağıdan tüm klasör ve dosyaları seçerek
           projemize ekleyip css ve js referanslarını view'larda section Scripts ekleyerek kullanabiliyoruz.
        */
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("MyError", "Id is required!");
            var result = _reviewService.Delete(id.Value);
            if (result == ResultStatus.Success)
            {
                TempData["Message"] = "Review deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View("MyError"); // exception result status
        }

        //private bool ReviewExists(int id)
        //{
        //    return _context.Reviews.Any(e => e.Id == id);
        //}
    }
}
