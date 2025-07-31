using SupplierService.Models;

namespace SupplierService.Interfaces
{
    public interface ISupplierService
    {
        List<SupplierInfo> GetMaterialInfo();
        List<GetRolesDetails> GetRoleInfo();
        Task<int> AddRoleData(AddRoleData user);
        
        Task<int> UpdateRoleData(UpdateRoleData user);
        Task<bool> UploadBomData(IFormFile excelFile);
        List<GetMenu> GetMenu();

        List<POHeader> GetPOHeader(string? SupplierCode = null);

        List<PONumbers> GetPONumber(string? SupplierCode = null);

        //List<POItemDetails> GetPOItemDetails(int ponumber);
        List<POLotDetails> GetPoLotDetailsAll();
        List<POItemLotDetails> GetPODetails(string ponumber);

        Task<bool> UpsertLotDetails(List<UpsertLotDetails> POList);

        Task<int> DeleteLotNumber(string PONumber, int ItemNo, int LotNumber, string Reason, int qty, int userID);

        List<Usermastercs> Getusermaster();

        List<Getrole> Getroles();
        Task<int> Adduserdata(Adduser user);
        Task<int> UpdateUserData(Updateuser user);

        Task<int> DeleteUserData(int userId);
        List<podetails> podetails();
        //List<GetRolesDetails> GetRoleInfo();
        //Task<int> AddRoleData(AddRoleData user);
        //Task<int> UpdateRoleData(UpdateRoleData user);
        //List<GetMenu> GetMenu();

        List<SupplierDetails> GetSupplierDetails();

        public List<Getdocuploaddetails> Getdocuploaddata(string pono, string itemno, int lotno);
        public void docrevision(string filename, string pono, string itemno, int lotno);
        public int uploaddocdetails(docuploaddetails data);
        public bool approvedoc(int docid);
        public bool rejectdoc(int docid,string remark);


    }

}
