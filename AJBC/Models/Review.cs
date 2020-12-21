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

        public string Name { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public byte[] Picture { get; set; }

        public string Message { get; set; }
    }
}
