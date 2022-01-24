using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly DbContext _db;

        public DirectorService(DbContext db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Set<Director>().Include(d => d.MovieDirectors).OrderBy(d => d.Name).ThenBy(d => d.Surname).Select(d => new DirectorModel()
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                Retired = d.Retired,

                FullName = d.Name + " " + d.Surname,
                RetiredModel = d.Retired ? "Yes" : "No",
                MovieCountModel = d.MovieDirectors.Count(), // her bir MovieDirector'ın bir Movie'si olduğundan MovieDirectors Count'ı kullanabiliriz.
                MoviesModel = d.MovieDirectors.Select(md => new MovieModel() // yönetmenin filmlerinin adlarını göstereceğimizden sadece MovieModel Name ve Id özelliklerini set etmemiz yeterli.
                {
                    Id = md.Movie.Id,
                    Name = md.Movie.Name,
                    ProductionYear = md.Movie.ProductionYear
                }).ToList(),
                MovieIds = d.MovieDirectors.Select(md => md.MovieId).ToList()
            });
        }

        public ResultStatus Add(DirectorModel model)
        {
            try
            {
                if (_db.Set<Director>().Any(d => d.Name.ToLower() == model.Name.ToLower().Trim() && d.Surname.ToLower() == model.Surname.ToLower().Trim()))
                    return ResultStatus.EntityExists;

                Director entity = new Director()
                {
                    Name = model.Name.Trim(),
                    Surname = model.Surname.Trim(),
                    Retired = model.Retired,

                    MovieDirectors = model.MovieIds?.Select(mId => new MovieDirector()
                    {
                        MovieId = mId
                    }).ToList()
                };

                _db.Set<Director>().Add(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }

        public ResultStatus Update(DirectorModel model)
        {
            try
            {
                if (_db.Set<Director>().Any(d => d.Name.ToLower() == model.Name.ToLower().Trim() && d.Surname.ToLower() == model.Surname.ToLower().Trim() && d.Id != model.Id))
                    return ResultStatus.EntityExists;

                Director entity = _db.Set<Director>().Include(d => d.MovieDirectors).SingleOrDefault(d => d.Id == model.Id);

                _db.Set<MovieDirector>().RemoveRange(entity.MovieDirectors);

                entity.Name = model.Name.Trim();
                entity.Surname = model.Surname.Trim();
                entity.Retired = model.Retired;

                entity.MovieDirectors = model.MovieIds?.Select(mId => new MovieDirector()
                {
                    MovieId = mId
                }).ToList();

                _db.Set<Director>().Update(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;                
            }
        }

        public ResultStatus Delete(int id)
        {
            try
            {
                Director entity = _db.Set<Director>().Include(d => d.MovieDirectors).SingleOrDefault(d => d.Id == id);

                _db.Set<MovieDirector>().RemoveRange(entity.MovieDirectors);

                _db.Set<Director>().Remove(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }
            catch
            {
                return ResultStatus.Exception;
            }
        }
    }
}
