using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public class UserCurrency
    {
        [Key]
        public int Id { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}