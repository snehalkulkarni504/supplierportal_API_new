namespace SupplierService.Models
{
    public class Getdocuploaddetails
    {
        public int docid { get; set; }
        public int? DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public int Revision { get; set; }
        public DateTime UploadDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Remarks { get; set; }
        public int PoId { get; set; }
        public string FileName { get; set; }
        public string PONumber { get; set; }
        public string ItemNo { get; set; }
        public int LotNumber { get; set; }
        public int IsApproved { get; set; }
        public string tpslremarks { get; set; }
    }
}
