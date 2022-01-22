using System.ComponentModel.DataAnnotations;

namespace _036_MoviesMvcBilgeAdam.Entities
{
    public class Movie
    {
        public int Id { get; set; }



        //[StringLength(250)]
        //public string Name { get; set; }
        // yukarıdaki yerine aşağıdaki gibi yazılırsa daha uygun olur
        [Required]
        [StringLength(250)]
        public string? Name { get; set; }



        public short? ProductionYear { get; set; }

        public double? BoxOfficeReturn { get; set; }

        //public List<Director>? Directors { get; set; } // List<Director>? Directors yerine ilişkileri tutan entity, yani MovieDirector, üzerinden List<MovieDirector>? MovieDirectors tanımlanmalı
        public List<MovieDirector>? MovieDirectors { get; set; } 

        public List<Review>? Reviews { get; set; } 
    }
}