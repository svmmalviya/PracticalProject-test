using PracticalProject.AppContext.DBModels;
using System.ComponentModel.DataAnnotations;

namespace PracticalProject.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.Products = new List<ProductMaster>();
            this.Items = new List<InvItems>();
        }
        public List<ProductMaster> Products { get; set; }

        [Display(Name = "Total Amount")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer")]
        public string PartyName { get; set; }
        public List<InvItems> Items { get; set; }
    }

    public class InvItems 
    {
        public string inventory { get; set; }
        public string? invoiceid { get; set; }
        public string? customer { get; set; }
        public string? product { get; set; }
        public string rate { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
    }
}
