using InvoiceMEF.Models;
using InvoiceMEF.Repositories;
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

        //Repositories
        private IInvoiceRepository _invoiceRepository;
        private IItemLineRepository _itemLineRepository;

        // DbContext Setup
        private ApplicationDbContext _context;


        // User manager - attached to application DB context       
        protected UserManager<ApplicationUser> UserManager { get; set; }



        // Constructor
        public InvoicesController()
        {
            _context = new ApplicationDbContext();

            _invoiceRepository = new InvoiceRepository(_context);
            _itemLineRepository = new ItemLineRepository(_context);

            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            //Mef Init
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
            var invoices = _invoiceRepository.GetInvoices();

            return View(invoices);
        }

        // GET: Invoices/Details/id
        [Route("Invoices/Details/{id}")]
        public ActionResult Details(int id)
        {
            var itemLines = _itemLineRepository.GetItemLinesByUserId(id);

            return View(itemLines);
        }

        // GET: Create
        public ActionResult Create()
        {
            var formViewModel = new FormViewModel();

            return View(formViewModel);
        }


        // POST: Create
        [HttpPost]
        public ActionResult Create(FormViewModel formViewModel)
        {
            var itemLines = formViewModel.ItemLines;
            var totalPrice = itemLines.Sum(x => Convert.ToDecimal(x.SinglePrice) * Convert.ToInt32(x.Amount));


            // Invoice to Db
            var invoice = new Invoice()
            {
                ApplicationUser = GetCurrentUser(),
                BuyerName = formViewModel.Invoice.BuyerName,
                DateCreated = DateTime.Today,
                DateDue = formViewModel.Invoice.DateDue,
                TotalPrice = itemLines.Sum(x => Convert.ToDecimal(x.SinglePrice) * Convert.ToInt32(x.Amount)),
                TotalPriceAfterTax = Core.CalculateTax(totalPrice, formViewModel.TaxCountries[formViewModel.TaxCountriesValue].Text)

            };

            _invoiceRepository.InsertInvoice(invoice);
            _invoiceRepository.Save();




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

                _itemLineRepository.InsertItemLine(itemLine);
            }

            _itemLineRepository.Save();


            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser()
        {
            return UserManager.FindById(User.Identity.GetUserId());
        }

    }
}