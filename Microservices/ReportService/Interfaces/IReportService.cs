using ReportService.Models;

namespace ReportService.Interfaces
{
    public interface IReportService
    {
        List<Getpodetails> podetails();

        Task<List<TRN_Deletion_Details>> GetLotDeletionDetails();
    }
}
