namespace SupplierService.Models
{
    public class POLotDetails
    {
        public int lotnumber { get; set; }
        public int lotqty { get; set; }
        public string etd { get; set; }
        public string eta { get; set; }

        public string actualdispatch { get; set; }

        public string actualarrival { get; set; }


        public string attachment { get; set; }
        public string ponumber { get; set; }
        public string itemno { get; set; }

        public string materialdescription { get; set; }
        public string suppliarname { get; set; }
        public string status { get; set; }

        public string Materialcode { get; set; }

        public string LotMW { get; set; }
        public int LCL { get; set; }
        public int _20FeetGPContainer { get; set; }
        public int _40FeetGPHCContainers { get; set; }
        public int TotalCNTR { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal InvoiceVal_FC { get; set; }
        public string Currency { get; set; }
        public int InvoiceQty { get; set; }
        public string PhysicalStatus { get; set; }
        public string isDeliveryComplete { get; set; }
        public string approvalstatus { get; set; }
    }
}
