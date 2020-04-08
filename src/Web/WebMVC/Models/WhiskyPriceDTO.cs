using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class WhiskyPriceDTO
    {
        [Required]
        public string WhiskyId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public int PriceReferenceId { get; set; }

        [Required]
        public int PriceDateYear { get; set; }

        [Required]
        public int PriceDateMonth { get; set; }

        [Required]
        public int PriceDateDay { get; set; }

        [Required]
        public string Seller { get; set; }

        [Required]
        public int? PriceNumber { get; set; }
    }
}
