using InvoiceMEF.Models;
using System.Collections.Generic;

namespace InvoiceMEF.ViewModels
{
    public class FormViewModel
    {
        public Invoice Invoice { get; set; }

        public List<ItemLine> ItemLines { get; } = new List<ItemLine>();
    }
}