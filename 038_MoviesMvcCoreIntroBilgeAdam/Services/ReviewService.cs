using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DbContext _db;

        public ReviewService(DbContext db)
        {
            _db = db;
        }

        public IQueryable<ReviewModel> Query()
        {
            return _db.Set<Review>().Include(r => r.Movie).OrderByDescending(r => r.Date).ThenBy(r => r.Reviewer)
                .ThenBy(r => r.Movie.Name).ThenByDescending(r => r.Rating).Select(r => new ReviewModel()
                {
                    Id = r.Id,
                    Content = r.Content,
                    Date = r.Date,
                    Rating = r.Rating,
                    Reviewer = r.Reviewer,
                    MovieId = r.MovieId,

                    MovieModel = new MovieModel()
                    {
                        Id = r.Movie.Id,
                        Name = r.Movie.Name
                    },

                    DateModel = r.Date.ToString("MM/dd/yyyy", new CultureInfo("en-US")),
                    RatingCssClassModel = r.Rating >= 1 && r.Rating <= 3 ? "badge bg-danger" 
                        : r.Rating >= 4 && r.Rating <= 7 ? "badge bg-warning text-dark"
                        : "badge bg-success"
                });
        }

        public ResultStatus Add(ReviewModel model)
        {
            throw new NotImplementedException();
        }

        public ResultStatus Update(ReviewModel model)
        {
            throw new NotImplementedException();
        }

        public ResultStatus Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
