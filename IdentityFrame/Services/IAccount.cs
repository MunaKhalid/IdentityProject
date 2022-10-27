using IdentityFrame.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityFrame.Services
{
    public interface IAccount 
    {
         Task<IdentityResult> CreateAccount(SignUpModel signup);
        Task<SignInResult> signin(SignInModel signInModel);
        Task<IdentityResult> AddRole(RoleModel roleModel);
        Task LogOut();
        List<ApplicaionUser> GetUsers();
        Task<List<UserRoles>> GetRoles(string user_id);
        Task UpdateUserRole(List<UserRoles> liUsrRole);
    }
}
