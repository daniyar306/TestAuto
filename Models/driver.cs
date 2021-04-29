using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuto.Models
{
    public class driver:BaseEntity 
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string name { get; set; }

        [Display(Name = "Auto")]
        public long auto_id { get; set; }

        [Display(Name = "Auto")]
        public auto auto { get; set; }
    }
}
