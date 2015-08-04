using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrettyCash.Models
{
    public class DefaultCurrencyViewModel
    {
        [Display(Name = "Default currency")]
        public Guid CurrencyId { get; set; }
        public string UserId { get; set; }
        public int UserCurrencyId { get; set; }
        public IEnumerable<SelectListItem> Currencies { get; set; }
    }
}