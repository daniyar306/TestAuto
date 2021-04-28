using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuto.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class auto: BaseEntity
    {
        [Key]
        public long id { get; set; }

        [Required]
        public string brand { get; set; }
        [Required]
        public string model { get; set; }
    }
}
