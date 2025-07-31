using Microsoft.Data.SqlClient;
using ReportService.Constants;
using ReportService.Context;
using ReportService.Exceptions;
using ReportService.Interfaces;
using ReportService.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;


namespace ReportService.Repositories
{
    public class ReportRepositories : IReportService
    {
        private ReportServiceContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        private readonly ILogger<ReportRepositories> _logger;
        private string sp_error;

        public ReportRepositories(IConfiguration configuration, ILogger<ReportRepositories> logger, ReportServiceContext supplierServiceContext)
        {
            _dbContext = supplierServiceContext;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString(SystemConstants.DefaultDBConnection);
            this._logger = logger;
            //_context = context;
        }



        #region Get Methods


        public List<Getpodetails> podetails()
        {
            List<Getpodetails> podetails = new List<Getpodetails>();


            try
            {
                _logger.LogInformation("podetails API calls at: " + DateTime.Now.ToString());

                DataSet dspodetails = new DataSet();

                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand(SystemConstants.podetails, sqlconnection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(dspodetails);

                    if (dspodetails != null || dspodetails.Tables.Count > 0)
                    {
                        foreach (DataRowView row in dspodetails.Tables[0].DefaultView)
                        {
                            {

                                Getpodetails po_deatils = new Getpodetails
                                {
                                    suppliercode = row["suppliercode"] != DBNull.Value ? row["suppliercode"].ToString() : null,
                                    suppliername = row["suppliername"] != DBNull.Value ? row["suppliername"].ToString() : null,
                                    pono = row["pono"] != DBNull.Value ? row["pono"].ToString() : null,
                                    itemno = row["itemno"] != DBNull.Value ? (int?)Convert.ToInt32(row["itemno"]) : null,
                                    lotno = row["lotno"] != DBNull.Value ? (int?)Convert.ToInt32(row["lotno"]) : null,
                                    materialcode = row["materialcode"] != DBNull.Value ? row["materialcode"].ToString() : null,
                                    materialdes = row["materialdes"] != DBNull.Value ? row["materialdes"].ToString() : null,
                                    materialqty = row["materialqty"] != DBNull.Value ? (int?)Convert.ToInt32(row["materialqty"]) : null,
                                    materialuom = row["materialuom"] != DBNull.Value ? row["materialuom"].ToString() : null,
                                    eta = row["ETA"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ETA"]) : null,
                                    LotQty = row["LotQty"] != DBNull.Value ? (int?)Convert.ToInt32(row["LotQty"]):null,
                                    deliverystatus = row["deliverystatus"] != DBNull.Value ? row["deliverystatus"].ToString() : null,
                                    etd = row["ETD"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ETD"]) : null,
                                    ActualArrival= row["ActualArrival"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ActualArrival"]) : null,
                                    ActualDispatch= row["ActualDispatch"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ActualDispatch"]) : null,
                                };


                                podetails.Add(po_deatils);
                            }
                        }
                    }
                }
                return podetails;

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Getdivisionsite API error: " + ex.Message);
                throw new RepositoryException("Error Fetching Getdivisionsite.", ex);
            }


        }


        public async Task<List<TRN_Deletion_Details>> GetLotDeletionDetails()
        {
            return await _dbContext.TRN_Deletion_Details.ToListAsync();
        }




        #endregion



        #region Post Methods




        #endregion

    }
}
