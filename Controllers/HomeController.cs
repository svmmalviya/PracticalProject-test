using Microsoft.AspNetCore.Mvc;
using PracticalProject.AppContext;
using PracticalProject.AppContext.DBModels;
using PracticalProject.Interface_and_repos;
using PracticalProject.Models;
using System.Diagnostics;

namespace PracticalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInvoice invoice;
        private static string message = string.Empty;

        public HomeController(ILogger<HomeController> logger, IInvoice invoice)
        {
            _logger = logger;
            this.invoice = invoice;
            //Items = new List<InvItems>();
        }

        public IActionResult InvoiceList()
        {
            var invoicemodel = invoice.GetInvoices();
            ViewBag.msg = message;
            message = string.Empty;
            return View(invoicemodel);
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            var products = invoice.GetProducts();

            homeViewModel.Products = products;

            return View(homeViewModel);
        }


        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {

            var products = invoice.GetProducts();

            if (ModelState.IsValid)
            {
                model = new HomeViewModel();
            }

            model.Products = products;
            return View(model);
        }

        /// <summary>
        /// Get inventories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = invoice.GetProducts();

            return Json(products);
        }

        /// <summary>
        /// Edit invoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditInvoice(int id)
        {
            InvoiceEditModel invoicemodel = new InvoiceEditModel();
            var products = invoice.GetProducts();
            var invoiceObject = invoice.GetInvoice(id);

            invoicemodel.Products = products;

            if (invoiceObject != null)
            {

                invoicemodel.PartyName = invoiceObject.PartyName;
                invoicemodel.invoiceno = invoiceObject.InvoiceNo;

                invoicemodel.Items.AddRange(invoiceObject.invoiceDetails.Select(x => new InvItems
                {
                    amount = x.Amount.ToString(),
                    customer = invoiceObject.PartyName,
                    invoiceid = x.InvoiceDetailId.ToString(),
                    product = x.Product.ProductName,
                    inventory = x.Product.ProductId.ToString(),
                    qty = x.Quantity.ToString(),
                    rate = x.Rate.ToString(),
                }).ToList());
            }

            return View(invoicemodel);
        }



        [HttpGet]
        public IActionResult DeleteInvoice(int id)
        {
            var deleted = invoice.DeleteInvoice(id);

            if (!deleted)
            {
                message = "Unable to delete invoice.";
            }
            else
            {
                message = "Invoice deleted succesfully";
            }
            return RedirectToAction("InvoiceList");
        }


        [HttpPost]
        public IActionResult UpdateInvoice(InvoiceEditModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var updated = invoice.UpdateInvoice(model);

                message = updated == true ? "Invoice updated succesfully." : "Invoice updation failed.";
            }

            return Json(message);
        }

        [HttpPost]
        public IActionResult GenerateInvoice(HomeViewModel model)
        {
            var invoiceNo = 0;

            if (ModelState.IsValid)
            {
                invoiceNo = invoice.GenerateInvoice(model);
            }
            return Json(invoiceNo);
        }

        public IActionResult ViewInvoice(int invoiceNo)
        {
            var myinvoice = invoice.ViewInvoice(invoiceNo);
            return View(myinvoice);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}