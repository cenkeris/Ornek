using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CenkEris.ViewModels
{
    public class AddViewModel
    {
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string avatar { get; set; }
    }
}
