namespace SupplierService.Models
{
    public class POHeader
    {
        public int ID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string PONumber { get; set; }
        public string PODate { get; set; }
        public string DocType { get; set; }
        public string Status { get; set; }
        public string SupplierRemark { get; set; }
        public string TPSRemark { get; set; }
    }
}
