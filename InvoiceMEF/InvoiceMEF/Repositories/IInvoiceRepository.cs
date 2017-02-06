using InvoiceMEF.Models;
using System;
using System.Collections.Generic;

namespace InvoiceMEF.Repositories
{
    interface IInvoiceRepository : IDisposable
    {
        IEnumerable<Invoice> GetInvoices();
        void InsertInvoice(Invoice invoice);
        void Save();
    }
}
