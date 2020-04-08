using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class WhiskyImageDTO
    {
        [Required]
        public string WhiskyId { get; set; }

        [Required]
        public List<string> Urls { get; set; }
    }
}
