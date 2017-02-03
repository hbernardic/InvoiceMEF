using System.Collections.Generic;

namespace InvoiceMEF.Models
{
    public partial class ApplicationUser
    {
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}