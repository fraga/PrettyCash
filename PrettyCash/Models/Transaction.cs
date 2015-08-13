using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrettyCash.Models
{
    public class Transaction: ITraceable
    {
        [Key]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Amount in monetary standard
        /// </summary>
        [Display(Name = "Amount")]
        public decimal AmountMST { get; set; }

        /// <summary>
        /// Amount in currency always in USD
        /// </summary>
        public decimal AmountCur { get; set; }

        public virtual Currency Currency { get; set; }

        [StringLength(500)]
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime TransDate { get; set; }

        //traceable
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
    }
}