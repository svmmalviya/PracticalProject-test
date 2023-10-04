using System.ComponentModel.DataAnnotations;

namespace PracticalProject.AppContext.DBModels
{
    public class InvoiceMaster
    {
        [Key]
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string PartyName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public double TotalAmount { get; set; }

        public ICollection<InvoiceDetail> invoiceDetails { get; set; }
    }
}
