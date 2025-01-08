using Microsoft.AspNetCore.Mvc;
using ReportService.Exceptions;
using ReportService.Interfaces;
using ReportService.Models;

namespace ReportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _reportportal;

        public ReportController(IReportService reportportal, ILogger<ReportController> logger)
        {
            _reportportal = reportportal ?? throw new ArgumentNullException(nameof(reportportal));
            this._logger = logger;
        }



        #region GetMethod

        [HttpGet]
        [Route("/Getpodetailsreport")]
        public async Task<IActionResult> Getpodetails()
        {
            try
            {
                _logger.LogInformation("Getpodeatils API started at: " + DateTime.Now);
                List<Getpodetails> podetails = _reportportal.podetails();
                var result = podetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogInformation("Getdivisionsite API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Getdivisionsite API Error: ", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpGet]
        [Route("/GetLotDeletionDetails")]
        public async Task<IActionResult> GetLotDeletionDetails()
        {
            try
            {
                _logger.LogInformation("GetLotDeletionDetails API started at: " + DateTime.Now);
                List<TRN_Deletion_Details> podetails = await _reportportal.GetLotDeletionDetails();
                var result = podetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogInformation("GetLotDeletionDetails API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetLotDeletionDetails API Error: ", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }


        #endregion


        #region Postmethod


        #endregion


    }
}
