using PracticalProject.AppContext.DBModels;
using PracticalProject.Models;

namespace PracticalProject.Interface_and_repos
{
    public interface IInvoice
    {
        public int GenerateInvoice(HomeViewModel invoiceDetails);
        public bool UpdateInvoice(InvoiceEditModel invoiceDetails);
        public List<ProductMaster> GetProducts();
        public InvoiceViewModel ViewInvoice(int invoiceId);
        public InvoiceMaster GetInvoice(int invoiceId);
        public bool DeleteInvoice(int invoiceId);

        public InvoiceListView GetInvoices();
    }
}
