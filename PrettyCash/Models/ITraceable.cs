using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public interface ITraceable
    {
        DateTime CreatedDateTime { get; set; }
        DateTime? ModifiedDateTime { get; set; }
        ApplicationUser CreatedBy { get; set; }
        ApplicationUser ModifiedBy { get; set; }
    }
}