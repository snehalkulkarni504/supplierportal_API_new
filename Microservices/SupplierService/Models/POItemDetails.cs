namespace SupplierService.Models
{
    public class POItemDetails
    {
        
        public int itemno { get; set; }
        public string partcode { get; set; }
        public string description { get; set; }
        public int itemqty { get; set; }
        public string uom { get; set; }

        public bool expanded { get; set; }

        public List<POLotDetails> lotDetails { get; set; }

    }
}
