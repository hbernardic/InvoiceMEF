using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceMEF.Models
{
    public class ItemLine
    {
        public int ItemLineId { get; set; }

        [Required]
        public string Description { get; set; }

        public int Amount { get; set; }
        public decimal SinglePrice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}