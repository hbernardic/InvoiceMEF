using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceMEF.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateDue { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal? TotalPriceAfterTax { get; set; }

        public string BuyerName { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<ItemLine> Items { get; set; }
    }
}