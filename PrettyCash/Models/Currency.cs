using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public class Currency
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string ISO { get; set; }

        public string Name { get; set; }

        public string CurrencyDisplay { get { return string.Format("{0} - {1}", ISO, Name); } }

    }
}