using AdminService.Interfaces;
using AdminService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtpNet;

namespace AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            this._logger = logger;
        }

        [HttpGet]
        [Route("/GetMenu")]
        public async Task<IActionResult> GetMenu(int id)
        {
            try
            {
                _logger.LogInformation("GetMenu API started at:" + DateTime.Now);
                var result = _adminService.GetMenu(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpPost]
        [Route("/ValidateUserLogin")]
        public async Task<IActionResult> Login([FromBody] ValidateUserLogin loginRequest)
        {
            _logger.LogInformation($"Login {loginRequest}");
            try
            {
                List<ValidateUserLogin> result = await _adminService.ValidateUserLoginAsync(loginRequest.Username, loginRequest.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }


        [HttpGet]
        [Route("/Generate2FASecretcode")]
        public IActionResult Generate2FASecretcode(string email)
        {
            var secretKey = KeyGeneration.GenerateRandomKey(20);
            var base32Secret = Base32Encoding.ToString(secretKey);

            var totp = new Totp(secretKey);
            var otpAuthUrl = $"otpauth://totp/Tata Power:{email}?secret={base32Secret}&issuer=Tata Power&digits=6";
            var qrCodeImageUrl = $"https://api.qrserver.com/v1/create-qr-code/?data={Uri.EscapeDataString(otpAuthUrl)}";

            return Ok(new
            {
                secret = base32Secret,
                qrCodeUrl = qrCodeImageUrl
            });
        }


        [HttpPost]
        [Route("/Verify2FA")]
        public async Task<IActionResult> Verify2FA([FromBody] totpverify request)
        {
            _logger.LogInformation("Verify2fa controller called at" + DateTime.Now);
            var result = _adminService.verify2fa(request.id, request.otp);

            if (!result)
                return Unauthorized("Invalid TOTP code");

            return Ok(result);
        }



        [HttpGet]
        [Route("/Getusermaster")]
        public async Task<IActionResult> Getusermaster()
        {
            try
            {
                _logger.LogInformation("Getusermaster Controller started at:" + DateTime.Now);
                List<Usermaster> userdata = await _adminService.Getusermaster();
                return Ok(userdata);

            }
            catch (Exception ex)
            {
                _logger.LogError("Getusermaster Controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpGet]
        [Route("/Getroles")]
        public async Task<IActionResult> Getroles()
        {
            try
            {
                _logger.LogInformation("Getrole Controller started at:" + DateTime.Now);
                List<Getrole> roledata = await _adminService.Getroles();
                return Ok(roledata);

            }
            catch (Exception ex)
            {
                _logger.LogError("Getrole Controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }
        //[Route("/GetSuppliers")]
        //public async Task<IActionResult> GetSuppliers()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Getrole Controller started at:" + DateTime.Now);
        //        List<GetSupplier> SupplierData = await _adminService.GetSupplier();
        //        return Ok(SupplierData);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Getrole Controller Error :", ex.Message);
        //        return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
        //    }
        //}

        [HttpGet]
        [Route("/GetSuppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                _logger.LogInformation("GetSuppliers Controller started at:" + DateTime.Now);
                List<GetSupplier> SupplierData = _adminService.GetSupplier();
                return Ok(SupplierData);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetCountryDetails Controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpPost]
        [Route("/Adduser")]
        public async Task<IActionResult> Addusers([FromBody] Adduser user)
        {
            try
            {
                _logger.LogInformation("GetMenu API started at:" + DateTime.Now);
                var result = await _adminService.Addusers(user);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpPost]
        [Route("/UpdateUserData")]
        public async Task<IActionResult> Updateusers([FromBody] Updateuser user)
        {
            try
            {
                _logger.LogInformation("Update API started at:" + DateTime.Now);
                var result = await _adminService.UpdateUser(user);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Update API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpPost]
        [Route("/DeleteUserData")]
        public async Task<IActionResult> DeleteUserData([FromBody] int userId)
        {
            try
            {
                _logger.LogInformation("DeleteUserData API started at:" + DateTime.Now);
                var result = await _adminService.DeleteUser(userId);
                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError("DeleteUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpGet]
        [Route("/GetSupplierDetails")]
        public async Task<IActionResult> GetSupplierDetails()
        {
            try
            {
                _logger.LogInformation("GetSupplierDetails Controller started at:" + DateTime.Now);
                List<GetSupplierDetails> supplierdata = await _adminService.GetSupplierDetails();
                return Ok(supplierdata);

            }
            catch (Exception ex)
            {
                _logger.LogError("Getusermaster Controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }
        [HttpGet]
        [Route("/GetCountryDetails")]
        public async Task<IActionResult> GetCountryDetails()
        {
            try
            {
                _logger.LogInformation("GetCountryDetails Controller started at:" + DateTime.Now);
                List<GetCountryDetails> countrydata = _adminService.GetCountryDetails();
                return Ok(countrydata);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetCountryDetails Controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }
        [HttpPost]
        [Route("/Addsupplier")]
        public async Task<IActionResult> Addsupplier([FromBody] Addsupplier user)
        {
            try
            {
                _logger.LogInformation("Addsupplier controller started at:" + DateTime.Now);
                var result = await _adminService.Addsupplier(user);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Addsupplier controller Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpPost]
        [Route("/UpdateSupplierData")]
        public async Task<IActionResult> UpdateSupplierData([FromBody] UpdateSupplier user)
        {
            try
            {
                _logger.LogInformation("Update API started at:" + DateTime.Now);
                var result = await _adminService.UpdateSupplier(user);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError("Update API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpPost]
        [Route("/DeleteSupplierData")]
        public async Task<IActionResult> DeleteSupplierData([FromBody] int userId)
        {
            try
            {
                _logger.LogInformation("DeleteUserData API started at:" + DateTime.Now);
                var result = await _adminService.Deletesupplier(userId);
                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError("DeleteUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpGet]
        [Route("/GetRolesDetails")]
        public async Task<IActionResult> GetRolesDetails()
        {
            try
            {
                _logger.LogInformation("GetMaterialinfo API started at:" + DateTime.Now);
                var result = await _adminService.GetRoleInfo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpPost]
        [Route("/AddRoleData")]
        public async Task<IActionResult> Adduserdata([FromBody] AddRoleData user)
        {
            try
            {
                _logger.LogInformation("Getroles API started at:" + DateTime.Now);
                var result = await _adminService.AddRoleData(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("addroledata API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpPost]
        [Route("/UpdateRoleData")]
        public async Task<IActionResult> UpdateRoleData([FromBody] UpdateRoleData user)
        {
            try
            {
                _logger.LogInformation("UpdateRole API started at:" + DateTime.Now);
                var result = await _adminService.UpdateRoleData(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateRole API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

    }
}
