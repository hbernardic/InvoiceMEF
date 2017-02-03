using InvoiceMEF.Models;
using InvoiceMEF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
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



        //Actions
        //

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = _context.Invoices.ToList();

            return View(invoices);
        }

        // GET: Invoices/Details/id
        [Route("Invoices/Details/{id}")]
        public ActionResult Details(int id)
        {
            var itemLines = _context.ItemLines.Where(i => i.Invoice.InvoiceId == id).ToList();

            return View(itemLines);
        }

        // GET: Create
        public ActionResult Create()
        {
            var formViewModel = new FormViewModel();

            return View(formViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormViewModel formViewModel)
        {

            var user = UserManager.FindById(User.Identity.GetUserId());
            var itemLines = formViewModel.ItemLines;

            var invoice = new Invoice()
            {
                ApplicationUser = user,
                BuyerName = formViewModel.Invoice.BuyerName,
                DateCreated = DateTime.Today,
                DateDue = formViewModel.Invoice.DateDue

            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();




            foreach (var current in itemLines)
            {
                var itemLine = new ItemLine()
                {
                    Description = current.Description,
                    Amount = current.Amount,
                    SinglePrice = current.SinglePrice,
                    Invoice = invoice

                };

                _context.ItemLines.Add(itemLine);
            }

            invoice.TotalPrice = itemLines.Sum(x => Convert.ToDecimal(x.TotalPrice));
            _context.Invoices.Add(invoice);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}