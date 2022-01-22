using System.ComponentModel.DataAnnotations;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Models
{
    public class DirectorModel
    {
        #region Entity'den gelen özellikler
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? Surname { get; set; }

        public bool Retired { get; set; }
        #endregion


        
        public string? FullName { get; set; }
    }
}
