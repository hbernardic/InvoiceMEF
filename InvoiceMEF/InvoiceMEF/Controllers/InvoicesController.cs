using InvoiceMEF.Models;
using System.Linq;
using System.Web.Mvc;

namespace InvoiceMEF.Controllers
{
    public class InvoicesController : Controller
    {
        //DbContext Setup
        private ApplicationDbContext _context;

        public InvoicesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = _context.Invoices.ToList();

            return View(invoices);
        }

        [Route("Invoices/Details/{id}")]
        public ActionResult Details(int id)
        {
            var itemLines = _context.ItemLines.Where(i => i.Invoice.InvoiceId == id).ToList();

            return View(itemLines);
        }
    }
}