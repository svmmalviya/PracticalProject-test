using PracticalProject.AppContext.DBModels;
using System.ComponentModel.DataAnnotations;

namespace PracticalProject.Models
{
   
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            this.Items = new List<InvItems>();
        }

        public InvoiceMaster Invoice { get; set; }
        public List<InvItems> Items { get; set; }
    }

}
