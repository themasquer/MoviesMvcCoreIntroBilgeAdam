using _038_MoviesMvcCoreIntroBilgeAdam.Models;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Services.Bases
{
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
        ResultStatus Add(DirectorModel model);
        ResultStatus Update(DirectorModel model);
        ResultStatus Delete(int id);
    }
}
