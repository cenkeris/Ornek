using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CenkEris.Models
{
    public class Logs
    {
        public int id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required]
        public string OperationType { get; set; }
        [Required]
        public string Request { get; set; }
        [Required]
        public string Response { get; set; }
        [Required]
        public DateTime datetime { get; set; }

    }
}
