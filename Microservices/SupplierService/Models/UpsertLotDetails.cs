namespace SupplierService.Models
{
    public class UpsertLotDetails
    {
        public string PONumber { get; set; }
        public int itemno { get; set; }
        public string UserId { get; set; }
        public int lotnumber { get; set; }
        public int lotqty { get; set; }
        public string etd { get; set; }
        public string eta { get; set; }
        public string actualdispatch { get; set; }
        public string actualarrival { get; set; }
        public string attachment { get; set; }
        public bool isEditing { get; set; }
        public bool isNew { get; set; }
    }
}
