namespace SupplierService.Models
{
    public class POItemDetails
    {
        public int itemno { get; set; }
        public string partcode { get; set; }
        public string description { get; set; }
        public int itemqty { get; set; }
        public string uom { get; set; }
        public string ImportDomestic { get; set; }
        public string PlantType { get; set; }
        public string Type { get; set; }
        public string GroupType { get; set; }
        public string VendorCode { get; set; }
        public string SupplierName { get; set; }
        public int POCumulativeOrdQty { get; set; }
        public decimal POCumulativeOrdMW { get; set; }
        public string InternalLCNo { get; set; }
        public string InternalLCDate { get; set; }
        public string ActLCNo { get; set; }
        public string ActLCDate { get; set; }
        public string BankName { get; set; }
        public string INCOTerms { get; set; }

        public List<POLotDetails> lotDetails { get; set; }

    }
}
