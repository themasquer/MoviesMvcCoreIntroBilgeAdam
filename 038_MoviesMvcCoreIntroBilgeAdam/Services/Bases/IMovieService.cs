using _038_MoviesMvcCoreIntroBilgeAdam.Models;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query(); // IQueryable çalıştırılmamış, sadece yazılan SQL sorgusu gibi düşünülebilir. ToList, SingleOrDefault, vb. methodlarla IQueryable sorgusu veritabanında çalıştırılır.
        ResultStatus Add(MovieModel model);
        ResultStatus Update(MovieModel model);
        ResultStatus Delete(int id);
    }
}
