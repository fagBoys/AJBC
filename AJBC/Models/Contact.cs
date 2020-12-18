using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AJBC.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
