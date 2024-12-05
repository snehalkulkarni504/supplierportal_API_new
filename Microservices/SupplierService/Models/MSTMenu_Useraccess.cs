
namespace SupplierService.Models
{
    public class MSTMenu_Useraccess
    {
        public int value { get; set; }
        public string? Menu { get; set; }
        public string text { get; set; }
        public bool? Status { get; set; }
        public string? MenuCategory { get; set; }
        public string? Navigate_Url { get; set; }
        public List<MSTMenu_Useraccess> children { get; set; }
    }
}
