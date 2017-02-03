using InvoiceMEF.Models;

namespace InvoiceMEF.ViewModels
{
    public class FormViewModel
    {
        public Invoice Invoice { get; set; }
        public ItemLine ItemLine { get; set; }
    }
}