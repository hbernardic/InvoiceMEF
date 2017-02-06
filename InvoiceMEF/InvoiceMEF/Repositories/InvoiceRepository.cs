using InvoiceMEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceMEF.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices.OrderByDescending(i => i.InvoiceId);
        }

        public void InsertInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}