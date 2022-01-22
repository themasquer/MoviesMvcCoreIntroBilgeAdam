using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _038_MoviesMvcCoreIntroBilgeAdam.Models
{
    public class MovieModel
    {
        #region Entity'den gelen özellikler
        public int Id { get; set; }
        
        [Required]
        [StringLength(250)]
        [DisplayName("Movie Name")]
        public string? Name { get; set; }

        [DisplayName("Production Year")]
        public short? ProductionYear { get; set; }

        public double? BoxOfficeReturn { get; set; }
        #endregion



        [DisplayName("Box Office Return")]
        public string? BoxOfficeReturnModel { get; set; }

        [DisplayName("Review Count")]
        public int? ReviewCountModel { get; set; }

        [DisplayName("Review Average")]
        public string? ReviewAverageModel { get; set; }

        [DisplayName("Directors")]
        public string? DirectorsModel { get; set; }

        [DisplayName("Directors")]
        public List<int>? DirectorIds { get; set; }
    }
}
