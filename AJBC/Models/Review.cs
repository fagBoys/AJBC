using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AJBC.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }


        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }
    }
}
