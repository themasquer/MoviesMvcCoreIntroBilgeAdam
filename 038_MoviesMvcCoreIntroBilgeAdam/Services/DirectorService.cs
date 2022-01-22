using _036_MoviesMvcBilgeAdam.Entities;
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
            return _db.Set<Director>().OrderBy(d => d.Name).ThenBy(d => d.Surname).Select(d => new DirectorModel()
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                Retired = d.Retired,

                FullName = d.Name + " " + d.Surname
            });
        }
    }
}
