﻿namespace ReportService.Models
{
    public class Getpodetails
    {
        public string? suppliercode { get; set; }
        public string? suppliername { get; set; }
        public string? pono { get; set; }
        public int? itemno { get; set; }
        public int? lotno { get; set; }
        public string? materialcode { get; set; }
        public string? materialdes { get; set; }
        public int? materialqty { get; set; }
        public int? LotQty {  get; set; }
        public string? materialuom { get; set; }
        public string? deliverystatus { get; set; }
        public DateTime? eta { get; set; }
        public DateTime? etd { get; set; }
        public DateTime? ActualArrival { get; set; }
        public DateTime? ActualDispatch {  get; set; }

    }
}
