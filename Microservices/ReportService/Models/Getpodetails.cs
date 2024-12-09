namespace ReportService.Models
{
    public class Getpodetails
    {
        public int? suppliercode { get; set; }
        public string? suppliername { get; set; }
        public int? pono { get; set; }
        public int? itemno { get; set; }
        public string? materialcode { get; set; }
        public string? materialdes { get; set; }
        public int? materialqty { get; set; }
        public string? materialuom { get; set; }
        public string? deliverystatus { get; set; }
        public DateTime? eta { get; set; }
        public DateTime? etd { get; set; }
    }
}
