using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SupplierService.Exceptions;
using SupplierService.Interfaces;
using SupplierService.Models;
using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;

namespace SupplierService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierPortal;

        public SupplierController(ISupplierService supplierPortal, ILogger<SupplierController> logger)
        {
            _supplierPortal = supplierPortal ?? throw new ArgumentNullException(nameof(supplierPortal));
            this._logger = logger;
        }

        #region Get Methods
        [HttpGet]
        [Route("/GetSupplierInfo")]
        public async Task<IActionResult> GetMaterialInfo()
        {
            try
            {
                _logger.LogInformation("GetMaterialinfo API started at:" + DateTime.Now);
                List<SupplierInfo> MaterialDetails = _supplierPortal.GetMaterialInfo();
                var result = MaterialDetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpGet]
        [Route("/GetPOHeaders")]
        public async Task<IActionResult> GetPOHeaders(string? suppliercode =null)
        {
            try
            {
                _logger.LogInformation("GetPOHeaders API started at:" + DateTime.Now);
                List<POHeader> POdetails = _supplierPortal.GetPOHeader(suppliercode);
                var result = POdetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetPOHeaders API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPOHeaders API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpGet]
        [Route("/GetPONumber")]
        public async Task<IActionResult> GetPONumber(string? suppliercode = null)
        {
            try
            {
                _logger.LogInformation("GetPONumber API started at:" + DateTime.Now);
                List<PONumbers> POnos = _supplierPortal.GetPONumber(suppliercode);
                var result = POnos;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetPONumber API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPONumber API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        //[HttpGet]
        //[Route("/GetPOItemDetails")]
        //public async Task<IActionResult> GetPOItemDetails(int PONumber)
        //{
        //    try
        //    {
        //        _logger.LogInformation("GetPOItemDetails API started at:" + DateTime.Now);
        //        List<POItemDetails> POItemdetails = _supplierPortal.GetPOItemDetails(PONumber);
        //        var result = POItemdetails;
        //        return Ok(result);
        //    }
        //    catch (RepositoryException ex)
        //    {
        //        _logger.LogError("GetPOItemDetails API Error :", ex.Message);
        //        return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("GetPOItemDetails API Error :", ex.Message);
        //        return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
        //    }

        //}

        [HttpGet]
        [Route("/GetPODetails")]
        public async Task<IActionResult> GetPODetails(int PONumber)
        {
            try
            {
                _logger.LogInformation("GetPODetails API started at:" + DateTime.Now);
                List<POItemLotDetails> POItemLotDetails = _supplierPortal.GetPODetails(PONumber);
                var result = POItemLotDetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetPODetails API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPODetails API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpGet]
        [Route("/Getusermaster")]
        public async Task<IActionResult> Getusermaster()
        {
            try
            {
                _logger.LogInformation("Getusermaster API started at:" + DateTime.Now);
                List<Usermastercs> userdetails = _supplierPortal.Getusermaster();
                var result = userdetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("Getusermaster API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("Getusermaster API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpGet]
        [Route("/Getroles")]
        public async Task<IActionResult> Getroles()
        {
            try
            {
                _logger.LogInformation("Getroles API started at:" + DateTime.Now);
                List<Getrole> roledetails = _supplierPortal.Getroles();
                var result = roledetails;
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("Getroles API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("Getroles API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpGet]
        [Route("/Getpodetailsreport")]
        public async Task<IActionResult> Getpodetailsreport()
        {
            try
            {
                _logger.LogInformation("Getpodeatils API started at: " + DateTime.Now);
                List<podetails> podetails = _supplierPortal.podetails();
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
        [Route("/GetRolesDetails")]
        public async Task<IActionResult> GetRolesDetails()
        {
            try
            {
                _logger.LogInformation("GetMaterialinfo API started at:" + DateTime.Now);
                List<GetRolesDetails> MaterialDetails = _supplierPortal.GetRoleInfo();
                var result = MaterialDetails;
                return Ok(result);

            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }

        [HttpGet]
        [Route("/GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            try
            {
                _logger.LogInformation("GetMaterialinfo API started at:" + DateTime.Now);
                List<GetMenu> MaterialDetails = _supplierPortal.GetMenu();
                var result = MaterialDetails;
                return Ok(result);

            }
            catch (RepositoryException ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("GetMaterailInfo API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }


        [HttpGet]
        [Route("/Getdocuploaddeatils/{pono}/{itemno}/{lotno}")]
        public async Task<IActionResult> Getdocupload(string pono,string itemno,int lotno)
        {
            try
            {
                _logger.LogInformation("this api calls at:" + DateTime.Now.ToString());
                List<Getdocuploaddetails> docupload = _supplierPortal.Getdocuploaddata(pono,itemno,lotno);
                var result = docupload;
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

        [HttpPost]
        [Route("/DownloadMultipleFiles")]
        public IActionResult DownloadMultipleFiles([FromBody] List<downloaddata> requests)
        {
            string basePath = @"D:\File";

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var request in requests)
                    {
                        string filePath = Path.Combine(basePath, request.poNumber.ToString(), request.itemNumber.ToString(), request.lotNumber.ToString(), request.fileName);

                        if (System.IO.File.Exists(filePath))
                        {
                            // Add the file to the zip
                            var zipEntry = zipArchive.CreateEntry(request.fileName, CompressionLevel.Fastest);
                            using (var entryStream = zipEntry.Open())
                            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"File not found: {filePath}");
                        }
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                // Return the zip file
                return File(memoryStream.ToArray(), "application/zip", "Documents.zip");
            }
        }

        #endregion

        #region Post Methods
        [HttpPost]
        [Route("/UpsertLotDetails")]
        public async Task<IActionResult> UpsertLotDetails(List<UpsertLotDetails> Lotdets)
        {
            try
            {
                //string errorMessage = "";
                _logger.LogInformation("UpsertLotDetails API started at:" + DateTime.Now);
                var result = _supplierPortal.UpsertLotDetails(Lotdets);
                if (result.Result)
                {
                    return Ok(new { status = "Success" });
                }
                else
                {
                    return BadRequest(new { status = "Failed" });
                }
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("UpsertLotDetails API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("UpsertLotDetails API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }
        [HttpPost]
        [Route("/Adduserdata")]
        public async Task<IActionResult> Adduserdata([FromBody] Adduser user)
        {
            try
            {
                _logger.LogInformation("Getroles API started at:" + DateTime.Now);
                var result = await _supplierPortal.Adduserdata(user);
                return Ok(new { message = "User added successfully!" }); ;
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("adduserdata API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("adduserdata API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpPost]
        [Route("/UpdateUserData")]
        public async Task<IActionResult> UpdateUserData([FromBody] Updateuser user)
        {
            try
            {
                _logger.LogInformation("UpdateUserData API started at:" + DateTime.Now);
                var result = await _supplierPortal.UpdateUserData(user);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("UpdateUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
        }

        [HttpGet]
        [Route("/GetSupplier")]
        public async Task<IActionResult> GetSupplier()
        {
            try
            {
                _logger.LogInformation("GetSupplier API started at:" + DateTime.Now);
                List<SupplierDetails> suppdets = _supplierPortal.GetSupplierDetails();
                var result = suppdets;
                 return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("UpdateUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateUserData API Error :", ex.Message);
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
                var result = await _supplierPortal.DeleteUserData(userId);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("DeleteUserData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteUserData API Error :", ex.Message);
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

                var result = await _supplierPortal.AddRoleData(user);

                return Ok(result);

            }

            catch (RepositoryException ex)

            {

                _logger.LogError("addroledata API Error :", ex.Message);

                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });

            }

            catch (Exception ex)

            {

                _logger.LogError("addroledata API Error :", ex.Message);

                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });

            }

        }
        [HttpPost]
        [Route("/UpdateRoleData")]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateRoleData user)
        {
            try
            {
                _logger.LogInformation("Getroles API started at:" + DateTime.Now);
                var result = await _supplierPortal.UpdateRoleData(user);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError("UpdateRoleData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateRoleData API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }

        }



        [HttpPost]
        [Route("/UploadDoc")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string docno, [FromForm] string doctype, [FromForm] string poNumber, [FromForm] string itemNumber, [FromForm] string lotNumber, [FromForm] string remarks, [FromForm] string updatedby)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {

                string rootPath = Path.Combine("D:\\", "File");


                string folderPath = Path.Combine(rootPath, poNumber, itemNumber, lotNumber);


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                string filename = file.FileName;

                string filePath = Path.Combine(folderPath, file.FileName);

                int revision = 0;

                if (System.IO.File.Exists(filePath))
                {
                    revision = 1;
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                if (revision == 1)
                {
                    _supplierPortal.docrevision(filename, docno, poNumber, itemNumber, Convert.ToInt32(lotNumber));
                }
                else
                {
                    docuploaddetails data = new docuploaddetails();
                    data.filename = filename;
                    data.documentno = docno;
                    data.documenttype = doctype;
                    data.pono = poNumber;
                    data.itemno = itemNumber;
                    data.lotno = Convert.ToInt32(lotNumber);
                    data.remarks = remarks;
                    data.updatedby = updatedby;
                    _supplierPortal.uploaddocdetails(data);
                }

                return Ok(new { message = "File uploaded successfully!", filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while uploading the file.", error = ex.Message });
            }
        }


        #endregion

        #region Delete Methods
        [HttpDelete]
        [Route("/DeleteLotDetail/{PONumber}/{ItemNo}/{LotNumber}")]
        public async Task<IActionResult> DeleteSelectedWbs(int PONumber, int ItemNo, int LotNumber)
        {
            try
            {
                var IsDeleted = await _supplierPortal.DeleteLotNumber(PONumber, ItemNo, LotNumber);
                return Ok(IsDeleted);
            }
            catch (RepositoryException ex)
            {
                _logger.LogInformation("Delete Lot Detail API Error :", ex.Message);
                return StatusCode(500, new { Message = ex.Message, Details = ex.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Delete Lot Detail API Error :", ex.Message);
                return StatusCode(500, new { Error = ex.Message, Message = ex.InnerException?.Message });
            }
        }

        #endregion

        #region Put Methods
        #endregion

        #region Patch Methods
        #endregion
    }


}
