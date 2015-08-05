using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Data { get; set; }

        public ExchangeRate()
        {
            DateTime = DateTime.UtcNow;
        }
    }
}