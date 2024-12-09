using AdminService.Context;
using AdminService.Interfaces;
using AdminService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AdminService.Repositories
{
    public class AdminRepository : IAdminService
    {
        private AdminServiceContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(IConfiguration configuration, ILogger<AdminRepository> logger, AdminServiceContext adminServiceContext)
        {
            _dbContext = adminServiceContext;
            _configuration = configuration;
            this._logger = logger;
        }

        public string GetMenu(int id)
       {
            _logger.LogInformation("GetMenus API started at:" + DateTime.Now);

            List<MSTMenu_Useraccess> childrenhResult = new List<MSTMenu_Useraccess>();

            string output = "";
            try
            {
                if (id > 0)
                {
                    IList<MSTMenu_Useraccess> _MSTMenuchildren = new List<MSTMenu_Useraccess>();

                    List<MSTMenu_Useraccess> _MSTMenu = new List<MSTMenu_Useraccess>();

                    _MSTMenu = (from a in _dbContext.MST_Menu
                                join b in _dbContext.RELRoleMenu on a.MenuId equals b.MenuId
                                where a.Status == true && a.MenuCategory == "M1" && b.RoleId == id

                                select new MSTMenu_Useraccess
                                {
                                    value = a.MenuId,
                                    Menu = a.Menu,
                                    label = a.SubMenu,
                                    routeLink = a.Navigate_Url,
                                    icon  = a.Icon

                                }).OrderBy(x => x.value).ToList();

                    for (int i = 0; i < _MSTMenu.Count; i++)
                    {
                        MSTMenu_Useraccess subMenus = new MSTMenu_Useraccess
                        {
                            value = _MSTMenu[i].value,
                            Menu = _MSTMenu[i].Menu,
                            label = _MSTMenu[i].label,
                            routeLink = _MSTMenu[i].routeLink,
                            icon = _MSTMenu[i].icon,
                            items = GetsubMenus(_MSTMenu[i].label, id)
                        };
                        childrenhResult.Add(subMenus);
                    }

                    output = JsonConvert.SerializeObject(childrenhResult, Newtonsoft.Json.Formatting.Indented);

                }

            }

            catch (Exception ex)
            {
                _logger.LogError("GetMenus API error :" + ex.Message);
            }
            return output;
        }

        public List<MSTMenu_Useraccess> GetsubMenus(string MenuName, int rollId)
        {
            _logger.LogInformation("GetsubMenus API started at:" + DateTime.Now);
            try
            {
                List<MSTMenu_Useraccess> _MSTMenus = new List<MSTMenu_Useraccess>();
                _MSTMenus = (from a in _dbContext.MST_Menu
                             join b in _dbContext.RELRoleMenu on a.MenuId equals b.MenuId
                             where a.Status == true && a.MenuCategory == "M2" && b.RoleId == rollId && a.Menu == MenuName
                             select new MSTMenu_Useraccess
                             {
                                 value = a.MenuId,
                                 Menu = a.Menu,
                                 label = a.SubMenu,
                                 routeLink = a.Navigate_Url

                             }).OrderBy(x => x.value).ToList();
                return _MSTMenus;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetsubMenus API error :" + ex.Message);
                throw;
            }

        }

        public async Task<List<ValidateUserLogin>> ValidateUserLoginAsync(string username, string password)
        {
            List<ValidateUserLogin> user = new List<ValidateUserLogin>();

            _logger.LogInformation("ValidationUserLogin API started at:" + DateTime.Now);
            try
            {
                user = await _dbContext.MST_Users.Where(x => x.Username == username && x.Password == password && x.IsActive == true)
                    .Select(x => new ValidateUserLogin
                    {
                        Username = x.Username,
                        mst_user_id = x.mst_user_id,
                        RoleId = x.RoleId,
                        Password = x.Password
                    }).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("LoginValidation API error :" + ex.Message);
                throw;
            }

            return user;
        }

        public async Task<List<Usermaster>> Getusermaster()
        {
            _logger.LogInformation("getComparisonData API started at:" + DateTime.Now);
            try
            {
                List<Usermaster> usermaster = new List<Usermaster>();

               // var param = new SqlParameter("@Id", Ids);

                var userDetails = await Task.Run(() => _dbContext.userMaster
                             .FromSqlRaw(@"exec GetUserDetails").ToListAsync());

               // usermaster.AddRange(userDetails);

                return userDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("getComparisonData API error :" + ex.Message);
                throw;
            }
        }
        public async Task<List<Getrole>> Getroles()
        {
            _logger.LogInformation("getComparisonData API started at:" + DateTime.Now);
            try
            {
                List<Getrole> Getroles = new List<Getrole>();

                // var param = new SqlParameter("@Id", Ids);

                var roledetails = await Task.Run(() => _dbContext.Getroles
                             .FromSqlRaw(@"exec GetRoles").ToListAsync());

               // roledetails.AddRange(roledetails);

                return roledetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("getComparisonData API error :" + ex.Message);
                throw;
            }
        }
        public async Task<bool> Addusers(Adduser user)
        {
            _logger.LogInformation("getComparisonData API started at:" + DateTime.Now);
            try
            {
                List<Adduser> Addusers = new List<Adduser>();

                var param = new List<SqlParameter>
                {
                     new SqlParameter("@UserName", user.UserName),
                     new SqlParameter("@FullName", user.FullName),
                     new SqlParameter("@EmailId", user.EmailId),
                     new SqlParameter("@RoleId", user.RoleId),
                     new SqlParameter("@IsActive", user.IsActive),
                     new SqlParameter("@CreatedBy", user.CreatedBy),
                     new SqlParameter("@SupplierId",user.SupplierId)

                };


                // Convert the parameter list to an array
                var userdata = await
                    _dbContext.Database
                        .ExecuteSqlRawAsync("exec AddUserData @UserName, @FullName, @EmailId,@RoleId,@IsActive, @CreatedBy, @SupplierId", param.ToArray());

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("getComparisonData API error :" + ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdateUser(Updateuser user)
        {
            _logger.LogInformation("UpdateUser API started at: " + DateTime.Now);
            try
            {
                // Create the list of SQL parameters
                var param = new List<SqlParameter>
        {
            new SqlParameter("@UserId", user.UserId),
            new SqlParameter("@UserName", user.UserName),
            new SqlParameter("@FullName", user.FullName),
            new SqlParameter("@EmailId", user.EmailId),
            new SqlParameter("@RoleId", user.RoleId),
            new SqlParameter("@IsActive", user.IsActive),
            new SqlParameter("ModifiedBy",user.ModifiedBy),
            new SqlParameter("@SupplierId",user.SupplierId)
        };

                // Execute the stored procedure
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateUser @UserId, @UserName, @FullName, @EmailId, @RoleId, @IsActive, @ModifiedBy,",
                    param.ToArray());

                return true; // Return true if rows were affected
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateUser API error: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> DeleteUser(int userId)
        {
            _logger.LogInformation("DeleteUser API started at: " + DateTime.Now);
            try
            {
                var param = new List<SqlParameter>
                {
                     new SqlParameter("@UserId", userId)
                };

                // Execute the stored procedure
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC Deleteuser @UserId",
                    param.ToArray());

                // Return true if rows were affected
                if (result > 0)
                {
                    _logger.LogInformation($"User with ID {userId} deleted successfully.");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"No user found with ID {userId}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUser API error: " + ex.Message);
                throw;
            }
        }
        public async Task<List<GetSupplierDetails>> GetSupplierDetails()
        {
            _logger.LogInformation("GetSupplierDetails API started at:" + DateTime.Now);
            try
            {
                List<GetSupplierDetails> supplierDetails = new List<GetSupplierDetails>();

                // var param = new SqlParameter("@Id", Ids);

                supplierDetails = await Task.Run(() => _dbContext.supplierDetails
                             .FromSqlRaw(@"exec GetSupplierDetails").ToListAsync());

                // usermaster.AddRange(userDetails);

                return supplierDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetSupplierDetails API error :" + ex.Message);
                throw;
            }
        }
        public List<GetCountryDetails> GetCountryDetails()
        {
            _logger.LogInformation("GetCountryDetails API started at:" + DateTime.Now);
            try
            {
                List<GetCountryDetails> countryDetails = new List<GetCountryDetails>();
                countryDetails = _dbContext.CountryDetails
                    .Select(x => new GetCountryDetails
                    {
                        CountryId = x.CountryId,
                        CountryName = x.CountryName,
                    }).ToList();

                return countryDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetCountryDetails API error :" + ex.Message);
                throw;
            }

        }
        public List<GetSupplier> GetSupplier()
        {
            _logger.LogInformation("GetSupplier API started at:" + DateTime.Now);
            try
            {
                List<GetSupplier> Supplier = new List<GetSupplier>();
                Supplier = _dbContext.MST_Supplier
                    .Where(s => s.IsActive == true)
                    .Select(s => new GetSupplier
                    {
                        SupplierId = s.SupplierId,
                       SupplierName = s.SupplierCode + " - " + s.SupplierName
                    }).ToList();

                return Supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetSupplier API error :" + ex.Message);
                throw;
            }

        }
        //public async Task<List<GetSupplier>> GetSupplier()
        //{
        //    try
        //    {
        //        var suppliers = await _dbContext.MST_Supplier
        //            .Where(s => s.IsActive)
        //            .Select(s => new GetSupplier
        //            {
        //                SupplierId = s.SupplierId,
        //                SupplierName = s.SupplierCode + " - " + s.SupplierName
        //            })
        //            .ToListAsync();

        //        return suppliers;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error fetching active suppliers: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<bool> Addsupplier(Addsupplier user)
        {
            _logger.LogInformation("Addsupplier API started at:" + DateTime.Now);
            try
            {
                List<Addsupplier> AddSuppliers = new List<Addsupplier>();

                var param = new List<SqlParameter>
                {
                     new SqlParameter("@SupplierCode", user.SupplierCode),
                     new SqlParameter("@SupplierName", user.SupplierName),
                     new SqlParameter("@CountryId", user.CountryId),
                     new SqlParameter("@IsActive", user.IsActive),
                     new SqlParameter("@CreatedBy", user.CreatedBy)

                };


                // Convert the parameter list to an array
                var result = await
                    _dbContext.Database
                        .ExecuteSqlRawAsync("exec Addsupplier @SupplierCode, @SupplierName, @CountryId,@IsActive, @CreatedBy", param.ToArray());

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Addsupplier API error :" + ex.Message);
                throw;
            }
        }
        public async Task<bool> UpdateSupplier(UpdateSupplier user)
        {
            _logger.LogInformation("UpdateSupplier API started at: " + DateTime.Now);
            try
            {
                // Create the list of SQL parameters
                var param = new List<SqlParameter>
        {
            new SqlParameter("@SupplierId", user.SupplierId),
            new SqlParameter("@SupplierCode", user.SupplierCode),
            new SqlParameter("@SupplierName", user.SupplierName),
            new SqlParameter("@CountryId", user.CountryId),
            new SqlParameter("@IsActive", user.IsActive),
            new SqlParameter("@ModifiedBy", user.ModifiedBy)
        };

                // Execute the stored procedure
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateSupplier @SupplierId, @SupplierCode, @SupplierName, @CountryId, @IsActive,@ModifiedBy",
                    param.ToArray());

                return true; // Return true if rows were affected
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateSupplier API error: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> Deletesupplier(int userId)
        {
            _logger.LogInformation("DeleteUser API started at: " + DateTime.Now);
            try
            {
                var param = new List<SqlParameter>
                {
                     new SqlParameter("@UserId", userId)
                };

                // Execute the stored procedure
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC Deletesupplier @UserId",
                    param.ToArray());

                // Return true if rows were affected
                if (result > 0)
                {
                    _logger.LogInformation($"User with ID {userId} deleted successfully.");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"No user found with ID {userId}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUser API error: " + ex.Message);
                throw;
            }
        }

        public async Task<List<GetRolesDetails>> GetRoleInfo()
        {
            // List<GetRolesDetails> materialInfos = new List<GetRolesDetails>();
            _logger.LogInformation("GetRoleData API started at:" + DateTime.Now);
            try
            {
                return await _dbContext.Roleresults.FromSqlRaw<GetRolesDetails>("GetRoleData").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetRoleData API Error:" + ex.Message);
                throw;
            }

        }
        public async Task<bool> AddRoleData(AddRoleData user)
        {
            _logger.LogInformation("AddRoleData API started at: " + DateTime.Now);
            try
            {
                // Iterate through MenuIds and call the stored procedure for each


                string menu = user.MenuId.Substring(0, user.MenuId.Length - 1);

                var param = new List<SqlParameter>();
                param.Add(new SqlParameter("@RoleName", user.Role_name));
                param.Add(new SqlParameter("@Description", user.Description));
                param.Add(new SqlParameter("@IsActive", user.IsActive));
                param.Add(new SqlParameter("@CreatedBy", user.CreatedBy));
                param.Add(new SqlParameter("@MenuId", menu));

                // Execute the stored procedure
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "exec AddRoleData @RoleName, @Description, @IsActive, @CreatedBy, @MenuId ", param.ToArray());



                return true; // Indicate success
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding role data: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateRoleData(UpdateRoleData user)
        {
            _logger.LogInformation("UpdateRoleData API started at: " + DateTime.Now);
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@RoleId", user.RoleId));
                parameter.Add(new SqlParameter("@RoleName", user.RoleName));
                parameter.Add(new SqlParameter("@Description", user.Description));
                parameter.Add(new SqlParameter("@MenuId", user.MenuId));
                parameter.Add(new SqlParameter("@ModifiedBy", user.ModifiedBy));
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateRoleData @RoleId,@RoleName,@Description,@MenuId,@ModifiedBy", parameter.ToArray());

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding role data: {ex.Message}");
                throw;
            }


        }


    }
}
