using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Models
{
    public class ReviewModel
    {
        #region Entity'den gelen özellikler
        public int Id { get; set; }

        public string? Content { get; set; }

        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}!")]
        [Required(ErrorMessage = "{0} is required!")]
        public int Rating { get; set; }

        //[StringLength(200)]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(200, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string? Reviewer { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Movie")]
        public int MovieId { get; set; }
        #endregion

        public MovieModel? MovieModel { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Date")]
        public string? DateModel { get; set; }

        public string? RatingCssClassModel { get; set; }
    }
}
