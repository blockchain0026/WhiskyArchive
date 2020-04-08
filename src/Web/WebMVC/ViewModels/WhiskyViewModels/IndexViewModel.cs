using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Pagination;

namespace WebMVC.ViewModels.WhiskyViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Whisky> Whiskies { get; set; }
        public IEnumerable<SelectListItem> Distilleries { get; set; }
        public string Distillery { get; set; }
        public string Name { get; set; }

        public string Bottler { get; set; }

     
        public string Vintage { get; set; }
        public string Bottled { get; set; }

        public int? StatedAge { get; set; }

        public string CaskType { get; set; }

        public string CaskNumber { get; set; }
        
        public int? NumberOfBottles { get; set; }

        public float? Strength { get; set; }

        public int? Size { get; set; }

        public string Market { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
