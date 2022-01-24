using _038_MoviesMvcCoreIntroBilgeAdam.Entities;
using Microsoft.EntityFrameworkCore;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Contexts
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Windows Authentication
            //optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=BA_MoviesCore;trusted_connection=true;");

            // SQL Server Authentication
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=BA_MoviesCore;user id=sa;password=sa;");
        }
    }
}
