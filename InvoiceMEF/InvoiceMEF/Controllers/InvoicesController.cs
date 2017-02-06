using InvoiceMEF.Models;
using InvoiceMEF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Plugins.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace InvoiceMEF.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        // Mef 
        private CompositionContainer _compositionContainer;

        [Import(typeof(ICore))]
        public ICore Core;


        // DbContext Setup
        private ApplicationDbContext _context;


        // User manager - attached to application DB context       
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // Constructor
        public InvoicesController()
        {
            _context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.Load("Plugins")));


            _compositionContainer = new CompositionContainer(aggregateCatalog);

            try
            {
                _compositionContainer.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException);
                throw;
            }
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

            var totalPrice = itemLines.Sum(x => Convert.ToDecimal(x.SinglePrice) * Convert.ToInt32(x.Amount));



            var invoice = new Invoice()
            {
                ApplicationUser = user,
                BuyerName = formViewModel.Invoice.BuyerName,
                DateCreated = DateTime.Today,
                DateDue = formViewModel.Invoice.DateDue,
                TotalPrice = itemLines.Sum(x => Convert.ToDecimal(x.SinglePrice) * Convert.ToInt32(x.Amount)),
                TotalPriceAfterTax = Core.CalculateTax(totalPrice, formViewModel.TaxCountries[formViewModel.TaxCountriesValue].Text)

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
                    TotalPrice = current.SinglePrice * current.Amount,
                    Invoice = invoice

                };

                _context.ItemLines.Add(itemLine);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}