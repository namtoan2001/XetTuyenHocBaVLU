using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Account;

namespace XetTuyenVLU.Interfaces
{
    public interface IAccountService
    {
        public List<AccountVM> GetAllAccounts();
        public List<VaiTro> GetAllRoles();
        public AccountVM GetAccountById(int Id);
        public Task<AccountLoginVM> Login(AccountLoginRequest request);
        public Task<int> CreateAccount(AccountRegisterRequest request);
        public Task<bool> EditAccount(AccountEditingRequest request);
        public Task<bool> DeleteAccount(int Id);
        public AccountVM GetAccountProfile();
        public Task<bool> EditAccountProfile(ProfileEditingRequest request);
        public Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
