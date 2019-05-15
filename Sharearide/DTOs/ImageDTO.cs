using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.DTOs
{
    public class ImageDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string URL { get; set; }
    }
}
