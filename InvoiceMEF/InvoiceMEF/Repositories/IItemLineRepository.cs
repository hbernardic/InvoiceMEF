using InvoiceMEF.Models;
using System;
using System.Collections.Generic;

namespace InvoiceMEF.Repositories
{
    interface IItemLineRepository : IDisposable
    {
        IEnumerable<ItemLine> GetItemLinesByUserId(int id);
        void InsertItemLine(ItemLine itemLine);
        void Save();
    }
}
