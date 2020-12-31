using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AJBC.ViewModel
{
    public class ReviewViewModel

    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public IFormFile Picture { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

    }
}
