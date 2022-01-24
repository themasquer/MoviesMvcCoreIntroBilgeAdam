namespace _038_MoviesMvcCoreIntroBilgeAdam.Entities
{
    public class MovieDirector
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; } 

        public int DirectorId { get; set; }
        public Director? Director { get; set; } 
    }
}