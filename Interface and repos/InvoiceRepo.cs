using Microsoft.EntityFrameworkCore;
using PracticalProject.AppContext;
using PracticalProject.AppContext.DBModels;
using PracticalProject.InvoiceUtility;
using PracticalProject.Models;

namespace PracticalProject.Interface_and_repos
{
    public class InvoiceRepo : IInvoice
    {
        private readonly AppDbContext _context;
        public InvoiceRepo(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// remove invoice 
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <returns>true if deleted succesfully</returns>
        public bool DeleteInvoice(int invoiceId)
        {
            var invoiceDeleted = false;
            try
            {
                var toBeDeletedInv = _context.Invoices.Find(invoiceId);
                if (toBeDeletedInv != null)
                {
                    _context.Invoices.Remove(toBeDeletedInv);
                    _context.SaveChanges();

                    invoiceDeleted = true;
                }
            }
            catch (Exception e)
            {

            }

            return invoiceDeleted;
        }


        /// <summary>
        /// Generate new invoice
        /// </summary>
        /// <param name="invoiceDetails">inventory items</param>
        /// <returns>invoice no</returns>
        public int GenerateInvoice(HomeViewModel invoiceDetails)
        {
            var invoiceid = 0;
            try
            {
                InvoiceGenerator.invoiceNumberCounter = _context.Invoices.ToList().Count() + 1;
                var invoiceNo = InvoiceGenerator.GenerateInvoiceNumber();

                double totalAmt = 0;

                totalAmt = invoiceDetails.Items.Sum(x => Convert.ToDouble(x.amount));

                InvoiceMaster invoiceMaster = new InvoiceMaster
                {
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = invoiceNo,
                    PartyName = invoiceDetails.PartyName,
                    TotalAmount = totalAmt,
                };

                var inv = _context.Invoices.Add(invoiceMaster);

                _context.SaveChanges();

                foreach (var item in invoiceDetails.Items)
                {
                    var invDetail = new InvoiceDetail
                    {
                        Amount = Convert.ToInt32(item.amount),
                        InvoiceId = inv.Entity.InvoiceId,
                        Quantity = Convert.ToInt32(item.amount),
                        ProductId = Guid.Parse(item.inventory),
                        Rate = Convert.ToDouble(item.rate),
                    };

                    _context.InvoiceDetails.Add(invDetail);
                }

                _context.SaveChanges();

                invoiceid = inv.Entity.InvoiceId;
            }
            catch (Exception e)
            {

            }

            return invoiceid;
        }

        /// <summary>
        /// Invoice master with inventory items
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <returns><inventory master/returns>
        public InvoiceMaster GetInvoice(int invoiceId)
        {
            var model = new InvoiceMaster();
            try
            {

                model = _context.Invoices
    .Include(i => i.invoiceDetails).ThenInclude(x => x.Product)
    .FirstOrDefault(i => i.InvoiceId == invoiceId);

            }
            catch (Exception e)
            {
            }

            return model;
        }


        /// <summary>
        /// Fetch all invoices
        /// </summary>
        /// <returns>invoice list</returns>
        public InvoiceListView GetInvoices()
        {
            var invoicelist = new InvoiceListView();
            try
            {
                var invoices = _context.Invoices.ToList();

                invoicelist.Invoices = invoices;
            }
            catch (Exception e)
            {

            }
            return invoicelist;
        }

        /// <summary>
        /// Fetches inventory products 
        /// </summary>
        /// <returns>list of products </returns>
        public List<ProductMaster> GetProducts()
        {
            return _context.Products.ToList();
        }
        /// <summary>
        /// updates invoice and inventory items
        /// </summary>
        /// <param name="invoiceDetails">inventory items</param>
        /// <returns>true if updated</returns>
        public bool UpdateInvoice(InvoiceEditModel invoiceDetails)
        {
            var updated = false;
            try
            {
                var invoiceNo = invoiceDetails.invoiceno;

                double totalAmt = 0;

                totalAmt = invoiceDetails.Items.Sum(x => Convert.ToDouble(x.amount));

                InvoiceMaster invoiceMaster = _context.Invoices.Where(x => x.InvoiceNo == invoiceNo).FirstOrDefault();


                if (invoiceMaster != null)
                {

                    invoiceMaster.PartyName = invoiceDetails.PartyName;
                    invoiceMaster.ModifiedDate = DateTime.Now;
                    invoiceMaster.TotalAmount = totalAmt;

                    _context.Update(invoiceMaster);

                    foreach (var item in invoiceDetails.Items)
                    {
                        if (item.invoiceid != null)
                        {
                            var toupdateitem = _context.InvoiceDetails.Find(Guid.Parse(item.invoiceid.Trim()));


                            toupdateitem.Amount = Convert.ToDouble(item.amount);
                            toupdateitem.Rate = Convert.ToDouble(item.rate);
                            toupdateitem.Quantity = Convert.ToInt32(item.qty);

                            _context.InvoiceDetails.Update(toupdateitem);
                        }
                        else
                        {

                            var invDetail = new InvoiceDetail
                            {
                                Amount = Convert.ToDouble(item.amount),
                                InvoiceId = invoiceMaster.InvoiceId,
                                Quantity = Convert.ToInt32(item.qty),
                                ProductId = Guid.Parse(item.inventory),
                                Rate = Convert.ToDouble(item.rate),
                            };

                            _context.InvoiceDetails.Add(invDetail);
                        }

                    }
                }
                _context.SaveChanges();
                updated = true;
            }
            catch (Exception e)
            {

            }

            return updated;
        }


        /// <summary>
        /// fetch invoice by id to view 
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <returns>invoice with items</returns>
        public InvoiceViewModel ViewInvoice(int invoiceId)
        {
            var invoice = new InvoiceViewModel();
            var items = new List<InvItems>();
            try
            {
                var invoiceHead = _context.Invoices.Find(invoiceId);

                if (invoiceHead != null)
                {

                    items.AddRange(_context.InvoiceDetails.Where(x => x.InvoiceId == invoiceHead.InvoiceId).Select(x => new InvItems
                    {
                        invoiceid = x.InvoiceDetailId.ToString(),
                        amount = x.Amount.ToString(),
                        product = x.Product.ProductName,
                        inventory = x.ProductId.ToString(),
                        rate = x.Rate.ToString(),
                        qty = x.Quantity.ToString()
                    }).ToList());
                }

                invoice.Items = items;
                invoice.Invoice = invoiceHead;
            }
            catch (Exception e)
            {

            }
            return invoice;
        }
    }
}
