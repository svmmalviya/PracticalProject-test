using System.ComponentModel.DataAnnotations;

namespace PracticalProject.AppContext.DBModels
{
    public class ProductMaster
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public ICollection<InvoiceDetail> invoiceDetails { get; set; }
    }
}
