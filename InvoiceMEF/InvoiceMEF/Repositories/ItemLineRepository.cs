using InvoiceMEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceMEF.Repositories
{
    public class ItemLineRepository : IItemLineRepository
    {
        private ApplicationDbContext _context;

        public ItemLineRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<ItemLine> GetItemLinesByUserId(int id)
        {
            return _context.ItemLines.Where(i => i.Invoice.InvoiceId == id).ToList();
        }

        public void InsertItemLine(ItemLine itemLine)
        {
            _context.ItemLines.Add(itemLine);
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