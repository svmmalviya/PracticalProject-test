using PracticalProject.AppContext.DBModels;
using System.ComponentModel.DataAnnotations;

namespace PracticalProject.Models
{
   
    public class InvoiceListView
    {
        public InvoiceListView()
        {
            this.Invoices = new List<InvoiceMaster>();
        }
        public List<InvoiceMaster> Invoices { get; set; }
    }

}
