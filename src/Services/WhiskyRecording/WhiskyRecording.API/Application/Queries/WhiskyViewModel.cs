using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Queries
{
    public class WhiskyPrice
    {
        public int WhiskyPriceNumber { get; set; }

        public string WhiskyId { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public string PriceReference { get; set; }

        public string Seller { get; set; }

        public int PriceDateYear { get; set; }

        public int PriceDateMonth { get; set; }

        public int PriceDateDay { get; set; }
    }

    public class Whisky
    {
        public string WhiskyId { get; set; }

        public string WhiskyNameChinese { get; set; }

        public string WhiskyNameEnglish { get; set; }

        public string Distillery { get; set; }

        public string Bottler { get; set; }

        #region WhiskyDetail
        public string Vintage { get; set; }
        public string Bottled { get; set; }

        public int? StatedAge { get; set; }

        public string CaskType { get; set; }

        public string CaskNumber { get; set; }

        public int? NumOfBottles { get; set; }

        public float Strength { get; set; }

        public int Size { get; set; }

        public string Market { get; set; }
        #endregion

        public float WhiskyBaseRating { get; set; }

        public string Notes { get; set; }

        public DateTime DateUpdated { get; set; }

        // See the property initializer syntax below. This
        // initializes the compiler generated field for this
        // auto-implemented property.
        public List<WhiskyPrice> WhiskyPrices { get; set; }
        public List<string> WhiskyImages { get; set; } 

    }


    public class Distillery
    {
        public string DistilleryId { get; set; }
        public string DistilleryNameCHT { get; set; }
        public string DistilleryNameCHS { get; set; }
        public string DistilleryNameEN { get; set; }

        public string Established { get; set; }

        public string Introdution { get; set; }

        public string SmwsCode { get; set; }
    }
}
