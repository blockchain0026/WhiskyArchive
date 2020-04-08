using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class CalculationResult
    {
        public decimal WinPrice { get; set; }

        public decimal Commission { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal Insurance { get; set; }

        public decimal PaymentSurcharge { get; set; }

        public decimal VAT { get; set; }

        public decimal Customs { get; set; }

        public decimal TobaccoAlcoholTax { get; set; }

        public decimal BusinessTax { get; set; }

        public decimal AdvancePayment { get; set; }

        public decimal Total { get; set; }

    }
}
