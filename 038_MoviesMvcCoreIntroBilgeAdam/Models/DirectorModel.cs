using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Models
{
    public class DirectorModel
    {
        #region Entity'den gelen özellikler
        public int Id { get; set; }

        //[Required]
        //[Required(ErrorMessage = "Director Name is required!")]
        [Required(ErrorMessage = "{0} is required!")] // {0} eğer varsa DisplayName'i (Director Name) yoksa property name'i (Name) kullanır.
        //[StringLength(100)]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")] // {1} data annotation'ın ilk parametresini (3) kullanır.
        [MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Director Name")]
        public string? Name { get; set; }

        //[Required]
        [Required(ErrorMessage = "{0} is required!")]
        //[StringLength(100)]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string? Surname { get; set; }

        public bool Retired { get; set; }
        #endregion


        
        [DisplayName("Director Name")]
        public string? FullNameModel { get; set; }

        [DisplayName("Retired")]
        public string? RetiredModel { get; set; }

        [DisplayName("Movie Count")]
        public int MovieCountModel { get; set; }

        [DisplayName("Movies")]
        public List<MovieModel>? MoviesModel { get; set; }

        [DisplayName("Movies")]
        public List<int>? MovieIdsModel { get; set; }
    }
}
