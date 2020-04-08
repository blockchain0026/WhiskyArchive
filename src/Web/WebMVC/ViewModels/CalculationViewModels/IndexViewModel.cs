using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.CalculationViewModels
{
    public class IndexViewModel
    {
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal WinPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Strength { get; set; }

        public int Size { get; set; }

        public int WinLots { get; set; }


        public bool Insurance { get; set; }


        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal ExchangeRate { get; set; }

        public bool UseAdvancePayment { get; set; }

        public string Auction { get; set; }
        public IEnumerable<SelectListItem> Auctions { get; set; }

        public List<CalculationResult> CalculationResults { get; } = new List<CalculationResult>();

    }
}
