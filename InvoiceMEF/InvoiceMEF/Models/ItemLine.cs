using System.ComponentModel.DataAnnotations;

namespace InvoiceMEF.Models
{
    public class ItemLine
    {
        public int ItemLineId { get; set; }

        [Required]
        public string Description { get; set; }

        public int Amount { get; set; }

        public decimal SinglePrice { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}