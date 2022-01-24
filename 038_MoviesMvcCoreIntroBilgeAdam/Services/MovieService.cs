using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using _038_MoviesMvcCoreIntroBilgeAdam.Models;
using _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services
{
    // Ekstra kaynaklar:
    // https://automapper.org

    public class MovieService : IMovieService
    {
        private readonly DbContext _db;

        public MovieService(DbContext db)
        {
            _db = db;
        }

        public IQueryable<MovieModel> Query()
        {
            return _db.Set<Movie>().Include(m => m.Reviews).Include(m => m.MovieDirectors).ThenInclude(md => md.Director).OrderByDescending(m => m.ProductionYear).ThenBy(m => m.Name).Select(m => new MovieModel()
            {
                Id = m.Id,
                Name = m.Name,
                ProductionYear = m.ProductionYear,
                BoxOfficeReturn = m.BoxOfficeReturn,

                // Bu özelliği create ve edit view'larında da kullanacağımızdan parasal olarak formatlamadan ondalık veri olarak null olabilecek şekilde set etmemiz daha iyi
                //BoxOfficeReturnModel = (m.BoxOfficeReturn ?? 0).ToString("C2", new CultureInfo("en-US")) 
                BoxOfficeReturnModel = m.BoxOfficeReturn.HasValue ? m.BoxOfficeReturn.Value.ToString(new CultureInfo("en-US")) : "",

                ReviewCountModel = m.Reviews.Count, // Movie ile ilişkili Reviews kolleksiyonuna ulaştığımız için yukarıda Include etmemiz gerekli (Entity Framework Eager Loading)
                ReviewAverageModel = m.Reviews.Count > 0 ? m.Reviews.Average(r => r.Rating).ToString("N1", new CultureInfo("en-US")) : "", // N1: 1 haneli ondalık ve binlik ayraçlı formatla

                DirectorsModel = string.Join("<br />", m.MovieDirectors.Select(md => md.Director.Name + " " + md.Director.Surname)), // Movie ile ilişkili MovieDirectors ve MovieDirectors ile ilişkili Director'a ulaştığımız için yukarıda önce MovieDirectors'ı Include daha sonra Directors'ı ThenInclude etmemiz gerekli

                //DirectorIds = m.MovieDirectors.Select(md => md.Director.Id).ToList()
                DirectorIds = m.MovieDirectors.Select(md => md.DirectorId).ToList()
            });
        }

        public ResultStatus Add(MovieModel model)
        {
            try
            {
                // tüm kayıtlarda girilen ada sahip kayıt var mı kontrolünü yapıyoruz.
                //Movie existingEntity = _db.Set<Movie>().SingleOrDefault(m => m.Name.ToUpper() == model.Name.ToUpper().Trim());
                //if (existingEntity != null)
                //    return ResultStatus.EntityExists;
                // bunun yerine Any LINQ methodu da kullanılabilir:
                if (_db.Set<Movie>().Any(m => m.Name.ToUpper() == model.Name.ToUpper().Trim()))
                    return ResultStatus.EntityExists;

                Movie entity = new Movie()
                {
                    //Id = 0, // yeni film kaydı olduğundan Id'yi 0 vermeye gerek yoktur.
                    Name = model.Name.Trim(),
                    ProductionYear = model.ProductionYear,

                    //MovieDirectors = (model.DirectorIds != null && model.DirectorIds.Count > 0) ? model.DirectorIds.Select(dId => new MovieDirector()
                    //{
                    //    //MovieId = 0, // MovieDirectors zaten movie entity içerisinde olduğundan MovieId'ye 0 vermeye gerek yoktur.
                    //    DirectorId = dId
                    //}).ToList() : null
                    //MovieDirectors = (model.DirectorIds ?? new List<int>()).Select(dId => new MovieDirector()
                    //{
                    //    //MovieId = 0, // MovieDirectors zaten movie entity içerisinde olduğundan MovieId'ye 0 vermeye gerek yoktur.
                    //    DirectorId = dId
                    //}).ToList()
                    MovieDirectors = model.DirectorIds?.Select(dId => new MovieDirector()
                    {
                        //MovieId = 0, // MovieDirectors zaten movie entity içerisinde olduğundan MovieId'ye 0 vermeye gerek yoktur.
                        DirectorId = dId
                    }).ToList()

                    // BoxOfficeReturnModel string'ini double'a TryParse ile dönüştürmek daha iyi çünkü kullanıcı yanlışlıkla harf girişi yapabilir.
                    //BoxOfficeReturn = !string.IsNullOrWhiteSpace(model.BoxOfficeReturnModel) ? Convert.ToDouble(model.BoxOfficeReturnModel.Replace(",", "."), CultureInfo.InvariantCulture) : null
                };

                if (!string.IsNullOrWhiteSpace(model.BoxOfficeReturnModel))
                {
                    double boxOfficeReturn;
                    if (double.TryParse(model.BoxOfficeReturnModel.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out boxOfficeReturn))
                    {
                        entity.BoxOfficeReturn = boxOfficeReturn;
                    }
                    else
                    {
                        return ResultStatus.StringToDoubleConversionFailed;
                    }
                }
                _db.Set<Movie>().Add(entity);
                _db.SaveChanges();
                return ResultStatus.Success;
            }

            //catch (Exception exc) // Exception tipindeki exc parametresini catch içerisinde kullanmadığımızdan direkt catch de yazılabilir.
            catch
            
            {
                return ResultStatus.Exception;
            }
        }

        public ResultStatus Update(MovieModel model)
        {
            try
            {
                // düzenlediğimiz kayıt dışında girilen ada sahip başka kayıt var mı kontrolü için m.Id != model.Id koşulunu ekledik.
                if (_db.Set<Movie>().Any(m => m.Name.ToUpper() == model.Name.ToUpper().Trim() && m.Id != model.Id))
                    return ResultStatus.EntityExists;

                Movie entity = _db.Set<Movie>().Include(m => m.MovieDirectors).SingleOrDefault(m => m.Id == model.Id);

                // Önce filmlerle ilişkili olan film yönetmen kayıtlarını siliyoruz, bu yüzden yukarıda include ettik.
                _db.Set<MovieDirector>().RemoveRange(entity.MovieDirectors);

                // Daha sonra film entity'sini güncelliyoruz.
                entity.Name = model.Name.Trim();
                entity.ProductionYear = model.ProductionYear;
                if (!string.IsNullOrWhiteSpace(model.BoxOfficeReturnModel))
                {
                    double boxOfficeReturn;
                    if (double.TryParse(model.BoxOfficeReturnModel.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out boxOfficeReturn))
                    {
                        entity.BoxOfficeReturn = boxOfficeReturn;
                    }
                    else
                    {
                        return ResultStatus.StringToDoubleConversionFailed;
                    }
                }
                entity.MovieDirectors = model.DirectorIds?.Select(dId => new MovieDirector()
                {
                    DirectorId = dId
                }).ToList();
                _db.Set<Movie>().Update(entity);
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
                Movie entity = _db.Set<Movie>().Include(m => m.MovieDirectors).Include(m => m.Reviews).SingleOrDefault(m => m.Id == id);

                // Önce filmlerle ilişkili olan film yönetmen kayıtlarını siliyoruz, bu yüzden yukarıda include ettik.
                _db.Set<MovieDirector>().RemoveRange(entity.MovieDirectors);

                // Daha sonra eğer filmin ilişkili olduğu yorumlar varsa silme işlemini yapmamamız ya da yorumları da silmemiz gerekiyor.
                //_db.Set<Review>().RemoveRange(entity.Reviews); // ilişkili yorumları da silme
                if (entity.Reviews != null && entity.Reviews.Count > 0) // ilişkili yorum varsa filmi silmeme
                {
                    return ResultStatus.RelationalEntitiesExist;
                }

                _db.Set<Movie>().Remove(entity);
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
