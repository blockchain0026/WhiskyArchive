using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class Whisky
    {
        public string WhiskyId { get; set; }

        [DisplayName("Whisky Name (Chinese)")]
        public string WhiskyNameChinese { get; set; }

        [DisplayName("Whisky Name (English)")]
        public string WhiskyNameEnglish { get; set; }

        public string Distillery { get; set; }

        public string Bottler { get; set; }

        #region WhiskyDetail
        public string Vintage { get; set; }
        public string Bottled { get; set; }

        [DisplayName("Stated Age")]
        public int? StatedAge { get; set; }

        [DisplayName("Cask Type")]
        public string CaskType { get; set; }

        [DisplayName("Cask Number")]
        public string CaskNumber { get; set; }

        [DisplayName("Number Of Bottles")]
        public int? NumOfBottles { get; set; }

        public float Strength { get; set; }

        public int Size { get; set; }

        public string Market { get; set; }
        #endregion

        [DisplayName("Whisky Base Rating")]
        public float WhiskyBaseRating { get; set; }

        public string Notes { get; set; }

        public DateTime DateUpdated { get; set; }

        // See the property initializer syntax below. This
        // initializes the compiler generated field for this
        // auto-implemented property.
        public List<WhiskyPrice> WhiskyPrices { get; } = new List<WhiskyPrice>();
        public List<string> WhiskyImages { get; } = new List<string>();
    }
}
