using _036_MoviesMvcBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Seed() // ~/Database/Seed
        {
            //MovieContext db = new MovieContext();
            //// veritabanı işlemleri...
            //db.Dispose();
            using (MoviesContext db = new MoviesContext())
            {
                // veritabanı işlemleri...
                List<Movie> movieList = new List<Movie>()
                {
                    new Movie()
                    {
                        Name = "Avatar",
                        ProductionYear = 2009,
                        BoxOfficeReturn = 9000000
                    },
                    new Movie()
                    {
                        Name = "Sherlock Holmes",
                        ProductionYear = 2009,
                        BoxOfficeReturn = 1000000
                    },
                    new Movie()
                    {
                        Name = "Law Abiding Citizen",
                        ProductionYear = 2009,
                        BoxOfficeReturn = 2000000
                    },
                    new Movie()
                    {
                        Name = "Aliens",
                        ProductionYear = 1986,
                        BoxOfficeReturn = 8000000
                    }
                };
                List<Director> directorList = new List<Director>()
                {
                    new Director()
                    {
                        Name = "James",
                        Surname = "Cameron",
                        Retired = true
                    },
                    new Director()
                    {
                        Name = "Guy",
                        Surname = "Ritchie",
                        Retired = false
                    },
                    new Director()
                    {
                        Name = "F. Gary",
                        Surname = "Gray",
                        Retired = false
                    }
                };
                List<Review> reviewList = new List<Review>()
                {
                    new Review()
                    {
                        Content = "Very good movie.",
                        Rating = 9,
                        Reviewer = "Çağıl Alsaç",
                        Date = DateTime.Parse("06.01.2022"),
                        Movie = movieList[0]
                    },
                    new Review() {Content = "Nice movie.", Rating = 7, Reviewer = "Leo Alsaç", Movie = movieList[0], Date = DateTime.Parse("31.12.2018")},
                new Review() {Content = "Not a bad movie.", Rating = 6, Reviewer = "Angel Alsaç", Movie = movieList[0], Date = DateTime.Parse("23.06.2017")},
                new Review() {Content = "Pretty successful!", Rating = 8, Reviewer = "Ali Veli", Movie = movieList[1], Date = DateTime.Parse("15.05.2020")},
                new Review() {Content = "Didn't like it.", Rating = 3, Reviewer = "Zeynep Can", Movie = movieList[2], Date = DateTime.Parse("21.10.2018")},
                new Review() {Content = "Superb!", Rating = 10, Reviewer = "Cem Tan", Movie = movieList[3], Date = DateTime.Parse("04.05.2016")},
                new Review() {Content = "A bad movie.", Rating = 2, Reviewer = "Cem Tan", Movie = movieList[1], Date = DateTime.Parse("19.08.2018")},
                new Review() {Content = "Bad movie.", Rating = 3, Reviewer = "Çağıl Alsaç", Movie = movieList[1], Date = DateTime.Parse("21.02.2020")},
                new Review() {Content = "A very bad movie.", Rating = 1, Reviewer = "Leo Alsaç", Movie = movieList[1], Date = DateTime.Parse("18.05.2018")},
                new Review() {Content = "A masterpiece.", Rating = 10, Reviewer = "Angel Alsaç", Movie = movieList[1], Date = DateTime.Parse("11.03.2016")},
                new Review() {Content = "Pretty good!", Rating = 6, Reviewer = "Ali Veli", Movie = movieList[2], Date = DateTime.Parse("19.05.2021")},
                new Review() {Content = "Liked it.", Rating = 6, Reviewer = "Zeynep Can", Movie = movieList[3], Date = DateTime.Parse("09.08.2017")},
                new Review() {Content = "Super!", Rating = 9, Reviewer = "Cem Tan", Movie = movieList[0], Date = DateTime.Parse("01.02.2021")},
                new Review() {Content = "A mediocre movie.", Rating = 5, Reviewer = "Cem Tan", Movie = movieList[0], Date = DateTime.Parse("30.05.2019")}
                };
                List<MovieDirector> movieDirectorList = new List<MovieDirector>()
                {
                    new MovieDirector() {Movie = movieList[0], Director = directorList[0]},
                    new MovieDirector() {Movie = movieList[1], Director = directorList[1]},
                    new MovieDirector() {Movie = movieList[2], Director = directorList[2]},
                    new MovieDirector() {Movie = movieList[3], Director = directorList[0]}
                };

                // database deletes:
                var movieDirectors = db.MovieDirectors.ToList();
                db.MovieDirectors.RemoveRange(movieDirectors);
                var reviews = db.Reviews.ToList();
                db.Reviews.RemoveRange(reviews);
                var movies = db.Movies.ToList();
                db.Movies.RemoveRange(movies);
                var directors = db.Directors.ToList();
                db.Directors.RemoveRange(directors);
                db.SaveChanges();

                // database inserts:
                db.MovieDirectors.AddRange(movieDirectorList);
                db.Reviews.AddRange(reviewList);
                db.SaveChanges();
            };

            return Content("<label style=\"color:red;\"><b>Database seed successful.</b></label>", "text/html");
        }
    }
}
