using AdminService.Models;

namespace AdminService.Interfaces
{
    public interface IAdminService
    {
        string GetMenu(int id);
        Task<List<ValidateUserLogin>> ValidateUserLoginAsync(string username, string password);
        Task<List<Usermaster>> Getusermaster();

        Task<List<Getrole>> Getroles();
        public Task<bool> Addusers(Adduser user);

        Task<bool> UpdateUser(Updateuser user);
        Task<bool> DeleteUser(int userId);
        Task<List<GetSupplierDetails>> GetSupplierDetails();
        List<GetCountryDetails> GetCountryDetails();
        Task<bool> Addsupplier(Addsupplier user);
        Task<bool> UpdateSupplier(UpdateSupplier user);
        Task<bool> Deletesupplier(int userId);

        Task<List<GetRolesDetails>> GetRoleInfo();
        Task<bool> AddRoleData(AddRoleData user);
        Task<bool> UpdateRoleData(UpdateRoleData user);
        //Task<List<GetSupplier>> GetSupplier();
        List<GetSupplier> GetSupplier();
        //List<GetSupplier> GetSupplier();


    }
}
