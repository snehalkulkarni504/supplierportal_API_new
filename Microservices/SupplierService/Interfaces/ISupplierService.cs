using SupplierService.Models;

namespace SupplierService.Interfaces
{
    public interface ISupplierService
    {
        List<SupplierInfo> GetMaterialInfo();
        List<GetRolesDetails> GetRoleInfo();
        Task<int> AddRoleData(AddRoleData user);
        Task<int> UpdateRoleData(UpdateRoleData user);
        List<GetMenu> GetMenu();

        List<POHeader> GetPOHeader(string? SupplierCode = null);

        List<PONumbers> GetPONumber(string? SupplierCode = null);

        //List<POItemDetails> GetPOItemDetails(int ponumber);

        List<POItemLotDetails> GetPODetails(int ponumber);

        Task<bool> UpsertLotDetails(List<UpsertLotDetails> POList);

        Task<int> DeleteLotNumber(int PONumber, int ItemNo, int LotNumber);

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

        public List<Getdocuploaddetails> Getdocuploaddata();
        public void docrevision(string filename, string documnetno, string pono, string itemno, int lotno);
        public int uploaddocdetails(docuploaddetails data);

    }

}
