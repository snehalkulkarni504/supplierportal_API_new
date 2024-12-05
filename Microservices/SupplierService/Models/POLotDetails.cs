namespace SupplierService.Models
{
    public class POLotDetails
    {
        public int lotnumber { get; set; }
        public int lotqty { get; set; }
        public string etd { get; set; }
        public string eta { get; set; }

        public string actualdispatch { get; set; }

        public string actualarrival {  get; set; }

        
        public string attachment { get; set; }
    }
}
