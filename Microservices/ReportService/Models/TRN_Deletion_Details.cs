namespace ReportService.Models
{
    public class TRN_Deletion_Details
    {
        public int ID { get; set; }                  // Primary Key, auto-increment
        public string? PONumber { get; set; }       // Purchase Order Number
        public string? ItemNumber { get; set; }     // Item Identifier
        public int? LotNumber { get; set; }         // Lot Identifier
        public int? ItemQty { get; set; }           // Quantity of Items
        public string? Remark { get; set; }         // Reason/Note for Deletion
        public DateTime? CreateDate { get; set; }   // Date of Record Creation
        public int? CreatedBy { get; set; }         // ID of the User who created the record

    }
}
