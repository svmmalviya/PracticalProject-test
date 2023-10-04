using System.ComponentModel.DataAnnotations;

namespace PracticalProject.AppContext.DBModels
{
    public class InvoiceDetail
    {
        [Key]
        public Guid InvoiceDetailId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public int? InvoiceId { get; set; }        
        public ProductMaster Product { get; set; }
        public InvoiceMaster invoice { get; set; }
    }
}
