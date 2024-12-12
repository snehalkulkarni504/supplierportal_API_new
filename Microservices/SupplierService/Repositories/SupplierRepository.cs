using Microsoft.Data.SqlClient;
using SupplierService.Constants;
using SupplierService.Context;
using SupplierService.Exceptions;
using SupplierService.Interfaces;
using SupplierService.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Threading.Channels;

namespace SupplierService.Repositories
{
    public class SupplierRepository : ISupplierService
    {
        private SupplierServiceContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        private readonly ILogger<SupplierRepository> _logger;
        private string sp_error;

        public SupplierRepository(IConfiguration configuration, ILogger<SupplierRepository> logger, SupplierServiceContext supplierServiceContext)
        {
            _dbContext = supplierServiceContext;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString(SystemConstants.DefaultDBConnection);
            this._logger = logger;
            //_context = context; 

        }

        public List<SupplierInfo> GetMaterialInfo()
        {
            List<SupplierInfo> materialInfos = new List<SupplierInfo>();
            DataSet dsmaterialinfo = new DataSet();

            try
            {
                _logger.LogInformation("API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.DefaultDBConnection, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsmaterialinfo);

                    if (dsmaterialinfo != null || dsmaterialinfo.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dsmaterialinfo.Tables[0].DefaultView)
                        {
                            SupplierInfo Matinfo = new SupplierInfo
                            {
                                Supplier_Name = Convert.ToString(datarow["Supplier_Name"]),
                                Material_Number = Convert.ToString(datarow["Material_Number"]),
                                Material_Description = Convert.ToString(datarow["material_desc"]),
                                MDM_Id = Convert.ToString(datarow["MDM_Id"]),
                                ERP_Id = Convert.ToString(datarow["ERP_Id"]),
                                Min_Qty = Convert.ToString(datarow["Min_Qty"]),
                                Max_Qty = Convert.ToString(datarow["Max_Qty"])
                            };
                            materialInfos.Add(Matinfo);
                        }


                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Getmaterialinfo API error: " + ex.Message);
                throw new RepositoryException("Error fetching Getmaterialinfo.", ex);
            }
            return materialInfos;
        }

        public List<POHeader> GetPOHeader(string? SupplierCode = null)
        {
            List<POHeader> poheaders = new List<POHeader>();
            DataSet dspoheader = new DataSet();

            try
            {
                _logger.LogInformation("GetPOHeader API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetPOHeader, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SupplierCode", SupplierCode));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dspoheader);

                    if (dspoheader != null || dspoheader.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dspoheader.Tables[0].DefaultView)
                        {
                            POHeader pohead = new POHeader
                            {
                                ID = datarow["ID"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ID"]),
                                SupplierCode = datarow["SupplierCode"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierCode"]),
                                SupplierName = datarow["SupplierName"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierName"]),
                                PONumber = datarow["PONumber"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["PONumber"]),
                                PODate = datarow["PODate"] == DBNull.Value ? DateTime.MinValue.ToString("dd-MM-yyyy") : Convert.ToDateTime(datarow["PODate"]).ToString("dd-MM-yyyy"),
                                DocType = datarow["DocType"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["DocType"]),
                                Status = datarow["Status"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["Status"]),
                                SupplierRemark = datarow["SupplierRemark"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierRemark"]),
                                TPSRemark = datarow["TPSRemark"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["TPSRemark"]),
                            };
                            poheaders.Add(pohead);
                        }


                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPOHeader API error: " + ex.Message);
                throw new RepositoryException("Error fetching GetPOHeader.", ex);
            }
            return poheaders;
        }

        public List<PONumbers> GetPONumber(string? SupplierCode = null)
        {
            List<PONumbers> ponoslist = new List<PONumbers>();
            DataSet dspono = new DataSet();

            try
            {
                _logger.LogInformation("GetPONumber API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetPONumber, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SupplierCode", SupplierCode));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dspono);

                    if (dspono != null || dspono.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dspono.Tables[0].DefaultView)
                        {
                            PONumbers ponos = new PONumbers
                            {
                                PONumber = datarow["PONumber"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["PONumber"]),
                                SupplierCode = datarow["SupplierCode"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierCode"]),
                            };
                            ponoslist.Add(ponos);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPONumber API error: " + ex.Message);
                throw new RepositoryException("Error fetching GetPONumber.", ex);
            }
            return ponoslist;
        }
        public List<GetRolesDetails> GetRoleInfo()
        {
            List<GetRolesDetails> materialInfos = new List<GetRolesDetails>();
            DataSet dsmaterialinfo = new DataSet();
            try
            {
                _logger.LogInformation("API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetRoleDetails, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsmaterialinfo);

                    if (dsmaterialinfo != null || dsmaterialinfo.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dsmaterialinfo.Tables[0].DefaultView)
                        {
                            GetRolesDetails Matinfo = new GetRolesDetails
                            {
                                menu_id = Convert.ToInt32(datarow["Rolemenuid"]),
                                id = Convert.ToInt32(datarow["Id"]),
                                Role = Convert.ToString(datarow["Role"]),
                                Description = Convert.ToString(datarow["Description"]),
                                Menu = Convert.ToString(datarow["Menu"]),
                                Status = Convert.ToString(datarow["Status"]),
                                CreatedBy = Convert.ToString(datarow["CreatedBy"]),
                                Created_Date = Convert.ToString(datarow["Created_Date"])
                            };
                            materialInfos.Add(Matinfo);
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Getmaterialinfo API error: " + ex.Message);
                throw new RepositoryException("Error fetching Getmaterialinfo.", ex);
            }
            return materialInfos;


        }
        public async Task<int> AddRoleData(AddRoleData user)
        {
            int Output = 0;
            string ErrorMessage = null;
            try
            {
                _logger.LogInformation("AdduserData called at " + DateTime.Now.ToString());
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open(); // Async Open
                    //FOR EACH START
                    foreach (var MenuId in user.MenuId)
                    {
                        var dynamicparameters = new DynamicParameters();
                        dynamicparameters.Add("@Role_name", user.Role_name);
                        dynamicparameters.Add("@Description", user.Description);
                        dynamicparameters.Add("@IsActive", user.IsActive);
                        dynamicparameters.Add("@MenuId", MenuId);
                        dynamicparameters.Add("@CreatedBy", user.CreatedBy);

                        // Calling the stored procedure
                        await sql.ExecuteAsync("AddRoleData", dynamicparameters, commandType: CommandType.StoredProcedure);
                        //Output = dynamicparameters.Get<int>("@Output");
                        // ErrorMessage = dynamicparameters.Get<string>("@ErrorMessage");
                        Output = 1;
                    }

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        _logger.LogError($"Failure in SP: {ErrorMessage}");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding user: {ex.Message}");
                throw new RepositoryException("Error occurred while adding user.", ex);
            }
            return Output;
        }
        public async Task<int> UpdateRoleData(UpdateRoleData user)
        {
            int Output = 0;
            string ErrorMessage = null;
            try
            {
                _logger.LogInformation("UpdateRoleData called at " + DateTime.Now.ToString());
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open(); // Async Open
                                //first reset the Menu Selections 
                    var dynamicparameters = new DynamicParameters();
                    dynamicparameters.Add("@RoleId", user.RoleId);
                    sql.ExecuteAsync(SystemConstants.DeleteRoleMenu, dynamicparameters, commandType: CommandType.StoredProcedure);
                    foreach (var MenuId in user.MenuId)
                    {


                        dynamicparameters.Add("@MenuId", MenuId);


                        // Calling the stored procedure
                        await sql.ExecuteAsync(SystemConstants.UpdateRole, dynamicparameters, commandType: CommandType.StoredProcedure);
                        //Output = dynamicparameters.Get<int>("@Output");
                        // ErrorMessage = dynamicparameters.Get<string>("@ErrorMessage");
                        Output = 1;
                    }





                    // Calling the stored procedure

                    // ErrorMessage = dynamicParameters.Get<string>("@ErrorMessage");

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        _logger.LogError($"Failure in SP: {ErrorMessage}");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating user: {ex.Message}");
                throw new RepositoryException("Error occurred while updating user.", ex);
            }
            return Output;
        }
        public List<GetMenu> GetMenu()
        {
            List<GetMenu> materialInfos = new List<GetMenu>();
            DataSet dsmaterialinfo = new DataSet();
            try
            {
                _logger.LogInformation("API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetMenu, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsmaterialinfo);

                    if (dsmaterialinfo != null || dsmaterialinfo.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dsmaterialinfo.Tables[0].DefaultView)
                        {
                            GetMenu Matinfo = new GetMenu
                            {
                                MenuId = Convert.ToInt32(datarow["MenuId"]),

                                Menu = Convert.ToString(datarow["Menu"]),

                                SubMenu = Convert.ToString(datarow["SubMenu"])
                            };
                            materialInfos.Add(Matinfo);
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Getmaterialinfo API error: " + ex.Message);
                throw new RepositoryException("Error fetching Getmaterialinfo.", ex);
            }
            return materialInfos;


        }


        //public List<POItemDetails> GetPOItemDetails(int ponumber)
        //{
        //    List<POItemDetails> POItemdets = new List<POItemDetails>();
        //    DataSet dspoitemdets = new DataSet();

        //    try
        //    {
        //        _logger.LogInformation("GetPOItemDetails API called at " + DateTime.Now.ToString());
        //        // _logger.LogInformation("Type" + Type);
        //        using (var sqlconnection = new SqlConnection(connectionString))
        //        {
        //            sqlconnection.Open();
        //            SqlCommand cmd = new SqlCommand(SystemConstants.GetPOItemDetails, sqlconnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@PONumber", ponumber));
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(dspoitemdets);

        //            if (dspoitemdets != null || dspoitemdets.Tables.Count > 0)
        //            {
        //                foreach (DataRowView datarow in dspoitemdets.Tables[0].DefaultView)
        //                {
        //                    POItemDetails POitems = new POItemDetails
        //                    {
        //                        ID = datarow["ID"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ID"]),
        //                        ponumber = datarow["PONumber"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["PONumber"]),
        //                        itemno = datarow["ItemNo"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ItemNo"]),
        //                        partcode = datarow["PartCode"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["PartCode"]),
        //                        description = datarow["Description"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["Description"]),
        //                        itemqty = datarow["ItemQty"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ItemQty"]),
        //                        uom = datarow["UOM"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["UOM"]),
        //                    };
        //                    POItemdets.Add(POitems);
        //                }


        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("GetPOItemDetails API error: " + ex.Message);
        //        throw new RepositoryException("Error fetching GetPOItemDetails.", ex);
        //    }
        //    return POItemdets;
        //}

        public List<POItemLotDetails> GetPODetails(int ponumber)
        {
            List<POItemLotDetails> POItemLotdetsList = new List<POItemLotDetails>();
            List<POItemDetails> POItemdets = new List<POItemDetails>();
            List<POLotDetails> POLotdets = new List<POLotDetails>();
            DataSet dspoitemdets = new DataSet();
            DataSet dspolotdets = new DataSet();
            POItemLotDetails POItemLotdets = new POItemLotDetails();

            try
            {
                _logger.LogInformation("GetPODetails API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    dspoitemdets = new DataSet();
                    POItemdets = new List<POItemDetails>();
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetPOItemDetails, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PONumber", ponumber));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dspoitemdets);

                    if (dspoitemdets != null || dspoitemdets.Tables.Count > 0)
                    {
                        POItemLotdets.PONumber = dspoitemdets.Tables[0].Rows[0]["PONumber"].ToString();
                        POItemLotdets.PODate = Convert.ToDateTime(dspoitemdets.Tables[0].Rows[0]["PODate"]).ToString("yyyy-MM-dd");
                        POItemLotdets.DocType = dspoitemdets.Tables[0].Rows[0]["DocType"].ToString();
                        foreach (DataRowView datarow in dspoitemdets.Tables[0].DefaultView)
                        {
                            POItemDetails POitems = new POItemDetails
                            {
                                itemno = datarow["ItemNo"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ItemNo"]),
                                partcode = datarow["PartCode"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["PartCode"]),
                                description = datarow["Description"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["Description"]),
                                itemqty = datarow["ItemQty"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["ItemQty"]),
                                uom = datarow["UOM"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["UOM"]),
                            };
                            dspolotdets = new DataSet();
                            POLotdets = new List<POLotDetails>();
                            cmd = new SqlCommand(SystemConstants.GetPOLotDetails, sqlconnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@PONumber", ponumber));
                            cmd.Parameters.Add(new SqlParameter("@ItemNo", Convert.ToInt32(datarow["ItemNo"])));
                            adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dspolotdets);
                            foreach (DataRowView datarowLots in dspolotdets.Tables[0].DefaultView)
                            {
                                POLotDetails POitemLots = new POLotDetails
                                {
                                    lotnumber = datarowLots["LotNumber"] == DBNull.Value ? 0 : Convert.ToInt32(datarowLots["LotNumber"]),
                                    lotqty = datarowLots["LotQty"] == DBNull.Value ? 0 : Convert.ToInt32(datarowLots["LotQty"]),
                                    etd = datarowLots["ETD"] == DBNull.Value ? DateTime.MinValue.ToString("yyyy-MM-dd") : Convert.ToDateTime(datarowLots["ETD"]).ToString("yyyy-MM-dd"),
                                    eta = datarowLots["ETA"] == DBNull.Value ? DateTime.MinValue.ToString("yyyy-MM-dd") : Convert.ToDateTime(datarowLots["ETA"]).ToString("yyyy-MM-dd"),
                                    actualdispatch = datarowLots["ActualDispatch"] == DBNull.Value ? DateTime.MinValue.ToString("yyyy-MM-dd") : Convert.ToDateTime(datarowLots["ActualDispatch"]).ToString("yyyy-MM-dd"),
                                    actualarrival = datarowLots["ActualArrival"] == DBNull.Value ? DateTime.MinValue.ToString("yyyy-MM-dd") : Convert.ToDateTime(datarowLots["ActualArrival"]).ToString("yyyy-MM-dd"),
                                };
                                POLotdets.Add(POitemLots);
                            }
                            POitems.lotDetails = POLotdets;
                            POItemdets.Add(POitems);
                        }
                        POItemLotdets.items = POItemdets;

                    }


                }

                POItemLotdetsList.Add(POItemLotdets);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPODetails API error: " + ex.Message);
                throw new RepositoryException("Error fetching GetPODetails.", ex);
            }
            return POItemLotdetsList;
        }

        public async Task<bool> UpsertLotDetails(List<UpsertLotDetails> POLotList)
        {
            bool isSuccess = false;
            try
            {
                
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    foreach (UpsertLotDetails POLots in POLotList)
                    {
                       
                            var dynamicParameters = new DynamicParameters();
                            dynamicParameters.Add("@PONumber", POLots.PONumber);
                            dynamicParameters.Add("@ItemNo", POLots.itemno);
                            dynamicParameters.Add("@UserId", POLots.UserId);
                            dynamicParameters.Add("@LotNumber", POLots.lotnumber);
                            dynamicParameters.Add("@LotQty", POLots.lotqty);
                            dynamicParameters.Add("@ETD", POLots.etd);
                            dynamicParameters.Add("@ETA", POLots.eta);
                            dynamicParameters.Add("@ActualDispatch", POLots.actualdispatch??null);
                            dynamicParameters.Add("@ActualArrival", POLots.actualarrival?? null);
                            sqlConnection.Query<int>(SystemConstants.UpsertPOLotDetails, dynamicParameters, commandType: CommandType.StoredProcedure);

                            isSuccess = true;
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return isSuccess;
        }
        public List<Usermastercs> Getusermaster()
        {
            List<Usermastercs> usermasters = new List<Usermastercs>();
            try
            {
                _logger.LogInformation("Getusermaster called at " + DateTime.Now.ToString());
                DataSet ds = new DataSet();

                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.Getuserdetails, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds);

                    if (ds != null || ds.Tables.Count > 0)
                    {
                        foreach (DataRowView drv in ds.Tables[0].DefaultView)
                        {
                            Usermastercs userinfo = new Usermastercs
                            {
                                Id = Convert.ToInt32(drv["Id"]),
                                UserName = Convert.ToString(drv["UserName"]),
                                Role = Convert.ToString(drv["Role"]),
                                Status = Convert.ToString(drv["Status"]),
                                Fullname = Convert.ToString(drv["Fullname"]),
                                EmaildId = Convert.ToString(drv["EmailId"]),
                                Created_by = Convert.ToString(drv["CreatedBy"]),
                                Created_Date = Convert.ToDateTime(drv["created_date"]),
                            };
                            usermasters.Add(userinfo);

                        }

                    }
                    return usermasters;

        #region Delete Methods
        //public async Task<int> DeleteLotNumber(int PONumber, int ItemNo, int LotNumber)
        //{
        //    try
        //    {
        //        int Output=0;
        //        string ErrorMessage = null;
        //        int result = -4;

        //        using (var sqlConnection = new SqlConnection(connectionString))
        //        {
        //            sqlConnection.Open();
        //            var dynamicParameters = new DynamicParameters();
        //            dynamicParameters.Add("@paPONumber", PONumber);
        //            dynamicParameters.Add("@paItemNo", ItemNo);
        //            dynamicParameters.Add("@paLotNumber", LotNumber);
        //            dynamicParameters.Add("@Output", null, dbType: DbType.Int32, ParameterDirection.Output);
        //            dynamicParameters.Add("@ErrorMessage", null, dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
        //            sqlConnection.Query<int>(SystemConstants.SP_DeleteLotDetails, dynamicParameters, commandType: CommandType.StoredProcedure);
        //            Output = dynamicParameters.Get<int>("@Output");
        //            ErrorMessage = dynamicParameters.Get<string>("@ErrorMessage");
        //            sqlConnection.Close();
        //            result = Output;
        //        }

        //        return result;

        //    }
        //    catch (RepositoryException ex)
        //    {
        //        throw new RepositoryException("Error occurred while Deleteing Lot Detail.", ex);
        //    }
        //}
        #endregion


                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Getmaterialinfo API error: " + ex.Message);
                throw new RepositoryException("Error fetching Getmaterialinfo.", ex);
            }
        }
        public List<Getrole> Getroles()
        {
            List<Getrole> roles = new List<Getrole>();
            try
            {
                _logger.LogInformation("Getrole called at " + DateTime.Now.ToString());
                DataSet ds1 = new DataSet();

                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetRoles, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds1);
                    if (ds1 != null || ds1.Tables.Count > 0)
                    {
                        foreach (DataRowView drv1 in ds1.Tables[0].DefaultView)
                        {
                            Getrole roleinfo = new Getrole
                            {
                                MST_Role_Id = Convert.ToInt32(drv1["MST_Role_Id"]),
                                RoleName = Convert.ToString(drv1["RoleName"]),
                                Description = Convert.ToString(drv1["Description"]),
                                IsActive = Convert.ToBoolean(drv1["IsActive"]),
                                CreatedBy = Convert.ToInt32(drv1["CreatedBy"]),
                                CreatedOn = Convert.ToDateTime(drv1["CreatedOn"]),
                                ModifiedBy = Convert.ToInt32(drv1["ModifiedBy"]),
                                ModifiedOn = Convert.ToDateTime(drv1["ModifiedOn"]),

                            };
                            roles.Add(roleinfo);
                        }
                    }
                    return roles;
                }


            }

            catch (Exception ex)
            {
                _logger.LogError("Getroles API error: " + ex.Message);
                throw new RepositoryException("Error fetching Getroles.", ex);
            }



        }
        public async Task<int> Adduserdata(Adduser user)
        {
            int Output = 0;
            string ErrorMessage = null;
            try
            {
                _logger.LogInformation("AdduserData called at " + DateTime.Now.ToString());
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open(); // Async Open
                    var dynamicparameters = new DynamicParameters();
                    dynamicparameters.Add("@UserName", user.UserName);
                    dynamicparameters.Add("@FullName", user.FullName);
                    dynamicparameters.Add("@EmailId", user.EmailId);
                    dynamicparameters.Add("@RoleId", user.RoleId);
                    dynamicparameters.Add("@IsActive", user.IsActive);
                    dynamicparameters.Add("@CreatedBy", user.CreatedBy);

                    // Calling the stored procedure
                    await sql.ExecuteAsync("AddUserData", dynamicparameters, commandType: CommandType.StoredProcedure);
                    //Output = dynamicparameters.Get<int>("@Output");
                    // ErrorMessage = dynamicparameters.Get<string>("@ErrorMessage");
                    Output = 1;

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        _logger.LogError($"Failure in SP: {ErrorMessage}");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding user: {ex.Message}");
                throw new RepositoryException("Error occurred while adding user.", ex);
            }
            return Output;
        }
        public async Task<int> UpdateUserData(Updateuser user)
        {
            int Output = 0;
            string ErrorMessage = null;
            try
            {
                _logger.LogInformation("UpdateUserData called at " + DateTime.Now.ToString());
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    sql.Open(); // Async Open
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UserId", user.UserId);
                    dynamicParameters.Add("@UserName", user.UserName);
                    dynamicParameters.Add("@FullName", user.FullName);
                    dynamicParameters.Add("@EmailId", user.EmailId);
                    dynamicParameters.Add("@RoleId", user.RoleId);
                    dynamicParameters.Add("@IsActive", user.IsActive);


                    // Calling the stored procedure
                    await sql.ExecuteAsync(SystemConstants.UpdateUserData, dynamicParameters, commandType: CommandType.StoredProcedure);
                    Output = 1;
                    // ErrorMessage = dynamicParameters.Get<string>("@ErrorMessage");

                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        _logger.LogError($"Failure in SP: {ErrorMessage}");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating user: {ex.Message}");
                throw new RepositoryException("Error occurred while updating user.", ex);
            }
            return Output;
        }
        public async Task<int> DeleteUserData(int userId) // Async method in repository
        {
            int output = 0;
            try
            {
                _logger.LogInformation("DeleteUserData called at " + DateTime.Now.ToString());

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync(); // Async open

                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UserId", userId);

                    // Execute the stored procedure
                    var rowsAffected = await sqlConnection.ExecuteAsync(SystemConstants.DeleteUserData, dynamicParameters, commandType: CommandType.StoredProcedure);

                    if (rowsAffected > 0)
                    {
                        output = 1; // Success
                    }
                    else
                    {
                        _logger.LogWarning($"No rows affected when attempting to delete user with ID {userId}.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting user: {ex.Message}");
                throw new RepositoryException("Error occurred while deleting user.", ex);
            }

            return output;
        }
        public List<podetails> podetails()
        {
            List<podetails> podetails = new List<podetails>();


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

                                podetails po_deatils = new podetails
                                {
                                    suppliercode = row["suppliercode"] != DBNull.Value ? (int?)Convert.ToInt32(row["suppliercode"]) : null,
                                    suppliername = row["suppliername"] != DBNull.Value ? row["suppliername"].ToString() : null,
                                    pono = row["pono"] != DBNull.Value ? (int?)Convert.ToInt32(row["pono"]) : null,
                                    itemno = row["itemno"] != DBNull.Value ? (int?)Convert.ToInt32(row["itemno"]) : null,
                                    materialcode = row["materialcode"] != DBNull.Value ? row["materialcode"].ToString() : null,
                                    materialdes = row["materialdes"] != DBNull.Value ? row["materialdes"].ToString() : null,
                                    materialqty = row["materialqty"] != DBNull.Value ? (int?)Convert.ToInt32(row["materialqty"]) : null,
                                    materialuom = row["materialuom"] != DBNull.Value ? row["materialuom"].ToString() : null,
                                    eta = row["ETA"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ETA"]) : null,

                                    deliverystatus = row["deliverystatus"] != DBNull.Value ? row["deliverystatus"].ToString() : null,
                                    etd = row["ETD"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ETD"]) : null,

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
       

        public List<SupplierDetails> GetSupplierDetails()
        {
            List<SupplierDetails> Supplierslist = new List<SupplierDetails>();
            DataSet dssupdets = new DataSet();

            try
            {
                _logger.LogInformation("GetSupplierDetails API called at " + DateTime.Now.ToString());
                // _logger.LogInformation("Type" + Type);
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand(SystemConstants.GetSupplier, sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dssupdets);

                    if (dssupdets != null || dssupdets.Tables.Count > 0)
                    {
                        foreach (DataRowView datarow in dssupdets.Tables[0].DefaultView)
                        {
                            SupplierDetails supp = new SupplierDetails
                            {
                                supplierid = datarow["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(datarow["SupplierId"]),
                                suppliercode = datarow["SupplierCode"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierCode"]),
                                suppliername = datarow["SupplierName"] == DBNull.Value ? string.Empty : Convert.ToString(datarow["SupplierName"])
                            };
                            Supplierslist.Add(supp);
                        }
                    }
                }
             }
                catch (Exception ex)
                {
                _logger.LogError("GetSupplierDetails API error: " + ex.Message);
                    throw new RepositoryException("Error fetching GetSupplierDetails.", ex);
                }
                return Supplierslist;
            }



        //public async Task<int> AddRoleData(AddRoleData user)
        //{
        //    int Output = 0;
        //    string ErrorMessage = null;
        //    try
        //    {
        //        _logger.LogInformation("AdduserData called at " + DateTime.Now.ToString());
        //        using (SqlConnection sql = new SqlConnection(connectionString))
        //        {
        //            sql.Open(); // Async Open
        //                        //FOR EACH START
        //            foreach (var MenuId in user.MenuId)
        //            {
        //                var dynamicparameters = new DynamicParameters();
        //                dynamicparameters.Add("@Role_name", user.Role_name);
        //                dynamicparameters.Add("@Description", user.Description);
        //                dynamicparameters.Add("@IsActive", user.IsActive);
        //                dynamicparameters.Add("@MenuId", MenuId);
        //                dynamicparameters.Add("@CreatedBy", user.CreatedBy);

        //                // Calling the stored procedure
        //                await sql.ExecuteAsync("AddRoleData", dynamicparameters, commandType: CommandType.StoredProcedure);
        //                //Output = dynamicparameters.Get<int>("@Output");
        //                // ErrorMessage = dynamicparameters.Get<string>("@ErrorMessage");
        //                Output = 1;
        //            }

        //            if (!string.IsNullOrEmpty(ErrorMessage))
        //            {
        //                _logger.LogError($"Failure in SP: {ErrorMessage}");
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occurred while adding user: {ex.Message}");
        //        throw new RepositoryException("Error occurred while adding user.", ex);
        //    }
        //    return Output;
        //}
        //public async Task<int> UpdateRoleData(UpdateRoleData user)
        //{
        //    int Output = 0;
        //    string ErrorMessage = null;
        //    try
        //    {
        //        _logger.LogInformation("UpdateRoleData called at " + DateTime.Now.ToString());
        //        using (SqlConnection sql = new SqlConnection(connectionString))
        //        {
        //            sql.Open(); // Async Open
        //                        //first reset the Menu Selections 
        //            var dynamicparameters = new DynamicParameters();
        //            dynamicparameters.Add("@RoleId", user.RoleId);
        //            sql.ExecuteAsync(SystemConstants.DeleteRoleMenu, dynamicparameters, commandType: CommandType.StoredProcedure);
        //            foreach (var MenuId in user.MenuId)
        //            {

        //                dynamicparameters.Add("@MenuId", MenuId);


        //                // Calling the stored procedure
        //                await sql.ExecuteAsync(SystemConstants.UpdateRole, dynamicparameters, commandType: CommandType.StoredProcedure);
        //                //Output = dynamicparameters.Get<int>("@Output");
        //                // ErrorMessage = dynamicparameters.Get<string>("@ErrorMessage");
        //                Output = 1;
        //            }




        //            // Calling the stored procedure
        //            // ErrorMessage = dynamicParameters.Get<string>("@ErrorMessage");

        //            if (!string.IsNullOrEmpty(ErrorMessage))
        //            {
        //                _logger.LogError($"Failure in SP: {ErrorMessage}");
        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occurred while updating user: {ex.Message}");
        //        throw new RepositoryException("Error occurred while updating user.", ex);
        //    }
        //    return Output;
        //}
        //public List<GetMenu> GetMenu()
        //{
        //    List<GetMenu> materialInfos = new List<GetMenu>();
        //    DataSet dsmaterialinfo = new DataSet();
        //    try
        //    {
        //        _logger.LogInformation("API called at " + DateTime.Now.ToString());
        //        // _logger.LogInformation("Type" + Type);
        //        using (var sqlconnection = new SqlConnection(connectionString))
        //        {
        //            sqlconnection.Open();
        //            SqlCommand cmd = new SqlCommand(SystemConstants.GetMenu, sqlconnection);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            adapter.Fill(dsmaterialinfo);

        //            if (dsmaterialinfo != null || dsmaterialinfo.Tables.Count > 0)
        //            {
        //                foreach (DataRowView datarow in dsmaterialinfo.Tables[0].DefaultView)
        //                {
        //                    GetMenu Matinfo = new GetMenu
        //                    {
        //                        MenuId = Convert.ToInt32(datarow["MenuId"]),
        //                        Menu = Convert.ToString(datarow["Menu"]),
        //                        SubMenu = Convert.ToString(datarow["SubMenu"])
        //                    };
        //                    materialInfos.Add(Matinfo);
        //                }


        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Getmaterialinfo API error: " + ex.Message);
        //        throw new RepositoryException("Error fetching Getmaterialinfo.", ex);
        //    }
        //    return materialInfos;


        //}

        public async Task<int> DeleteLotNumber(int PONumber, int ItemNo, int LotNumber)
        {
            try
            {
                int Output = 0;
                string ErrorMessage = null;
                int result = -4;

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@paPONumber", PONumber);
                    dynamicParameters.Add("@paItemNo", ItemNo);
                    dynamicParameters.Add("@paLotNumber", LotNumber);
                    dynamicParameters.Add("@Output", null, dbType: DbType.Int32, ParameterDirection.Output);
                    dynamicParameters.Add("@ErrorMessage", null, dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                    sqlConnection.Query<int>(SystemConstants.SP_DeleteLotDetails, dynamicParameters, commandType: CommandType.StoredProcedure);
                    Output = dynamicParameters.Get<int>("@Output");
                    ErrorMessage = dynamicParameters.Get<string>("@ErrorMessage");
                    sqlConnection.Close();
                    result = Output;
                }

                return result;

            }
            catch (RepositoryException ex)
            {
                throw new RepositoryException("Error occurred while Deleteing Lot Detail.", ex);
            }
        }

        public List<Getdocuploaddetails> Getdocuploaddata(string pono,string itemno,int lotno)
        {
            _logger.LogInformation($"Getdocuploaddata calls at " + DateTime.Now.ToString());
            List<Getdocuploaddetails> docuploads = new List<Getdocuploaddetails>();
            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();
                    var dynamicparameter = new DynamicParameters();
                    dynamicparameter.Add("pono", pono);
                    dynamicparameter.Add("itemno", itemno);
                    dynamicparameter.Add("lotno", lotno);

                    docuploads = (List<Getdocuploaddetails>)sqlconn.Query<Getdocuploaddetails>(SystemConstants.Getdocdetails, dynamicparameter, commandType: CommandType.StoredProcedure);
                }
                return docuploads;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Getdivisionsite API error: " + ex.Message);
                throw new RepositoryException("Error Fetching Getdivisionsite.", ex);

            }
        }

        public int uploaddocdetails(docuploaddetails data)
        {
            int Output = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@filename", data.filename);
                    dynamicParameters.Add("@documentno", data.documentno);
                    dynamicParameters.Add("@documenttype", data.documenttype);
                    dynamicParameters.Add("@pono", data.pono);
                    dynamicParameters.Add("@itemno", data.itemno);
                    dynamicParameters.Add("@lotno", data.lotno);
                    dynamicParameters.Add("@remarks", data.remarks);
                    dynamicParameters.Add("@updatedby", data.updatedby);



                    sqlConnection.QueryFirstOrDefault<int>(SystemConstants.uploaddocdetails, dynamicParameters, commandType: CommandType.StoredProcedure);
                    Output = 1;
                    sqlConnection.Close();

                    return Output;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void docrevision(string filename, string documnetno, string pono, string itemno, int lotno)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var dynamicParameters = new DynamicParameters();

                    dynamicParameters.Add("@filename", filename);
                    //dynamicParameters.Add("@documentno", documnetno);
                    dynamicParameters.Add("@pono", pono);
                    dynamicParameters.Add("@itemno", itemno);
                    dynamicParameters.Add("@lotno", lotno);

                    sqlConnection.QueryFirstOrDefault<int>(SystemConstants.docrevision, dynamicParameters, commandType: CommandType.StoredProcedure);
                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }


}
