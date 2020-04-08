using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class WhiskyDTO
    {
        [Required]
        public string DistilleryName { get; set; }

        [Required]
        public string WhiskyNameChinese { get; set; }

        [Required]
        public string WhiskyNameEnglish { get; set; }

        [Required]
        public string WhiskyBottler { get; set; }

        [Required]
        public string Vintage { get; set; }

        [Required]
        public string Bottled { get; set; }

        [Required]
        public int? StatedAge { get; set; }

        [Required]
        public string CaskType { get; set; }

        [Required]
        public string CaskNumber { get; set; }

        [Required]
        public int? NumberOfBottles { get; set; }

        [Required]
        public float? Strength { get; set; }

        [Required]
        public int? Size { get; set; }

        [Required]
        public string Market { get; set; }

        [Required]
        public float? Rating { get; set; }

        [Required]
        public string Notes { get; set; }



        [Required]
        public decimal? Price { get; set; }

        [Required]
        public int? CurrencyId { get; set; }

        [Required]
        public int? PriceReferenceId { get; set; }

        [Required]
        public int? PriceDateYear { get; set; }

        [Required]
        public int? PriceDateMonth { get; set; }

        [Required]
        public int? PriceDateDay { get; set; }

        [Required]
        public string Seller { get; set; }


        [Required]
        public List<string> ImageUrls { get; set; }


        [Required]
        public Guid RequestId { get; set; }
    }
}
