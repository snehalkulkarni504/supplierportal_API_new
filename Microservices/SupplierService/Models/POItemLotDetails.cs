namespace SupplierService.Models
{
    public class POItemLotDetails
    {
        public string PONumber { get; set; }
        public string PODate { get; set; }
        public string DocType { get; set; }
        public string isPODeleted { get; set; }

        public List<POItemDetails> items { get; set; }
    }
}
