using InvoiceMEF.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InvoiceMEF.ViewModels
{
    public class FormViewModel
    {
        public Invoice Invoice { get; set; }

        public List<ItemLine> ItemLines { get; } = new List<ItemLine>();

        public IList<SelectListItem> TaxCountries { get; } = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Croatia", Value = "0"},
            new SelectListItem() { Text = "Hungary", Value = "1"},
            new SelectListItem() { Text = "Serbia", Value = "2"},
            new SelectListItem() { Text = "Slovenia", Value = "3"}
        };

        public int TaxCountriesValue { get; set; }
    }
}