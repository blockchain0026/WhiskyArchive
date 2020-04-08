using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class WhiskyPrice
    {
        public int WhiskyPriceNumber { get; set; }

        public string WhiskyId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }

        [Required]
        public string PriceReference { get; set; }
        public IEnumerable<SelectListItem> PriceReferences { get; set; }

        public string Seller { get; set; }


        [Required]
        public int PriceDateYear { get; set; }

        [Required]
        public int PriceDateMonth { get; set; }

        [Required]
        public int PriceDateDay { get; set; }
    }
}
