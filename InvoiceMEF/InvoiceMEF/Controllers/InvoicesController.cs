using InvoiceMEF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace InvoiceMEF.Controllers
{
    public class InvoicesController : Controller
    {
        // DbContext Setup
        private ApplicationDbContext _context;

        // User manager - attached to application DB context       
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // Constructor
        public InvoicesController()
        {
            _context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        // Dispose _context
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

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(Invoice invoice)
        {
            /*var user = UserManager.FindById(User.Identity.GetUserId());

            var i = new Invoice()
            {
                ApplicationUser = user,
                BuyerName = invoice.BuyerName,
                DateCreated = DateTime.Today,
                DateDue = invoice.DateDue
            };

            _context.Invoices.Add(i);
            _context.SaveChanges();

            return RedirectToAction("NewItems", new { invoiceId = i.ApplicationUser.Id });*/

            return View();
        }
    }
}