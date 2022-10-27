using IdentityFrame.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace IdentityFrame.Services
{
    public class AccountServices : IAccount
    {
        //injection using constructor 
        UserManager<ApplicaionUser> userManager;
        SignInManager<ApplicaionUser> signIn;
        RoleManager<IdentityRole> roleManager;
        public AccountServices(UserManager<ApplicaionUser> _userManager, SignInManager<ApplicaionUser> _signIn, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signIn = _signIn;
            roleManager = _roleManager;
        }

        //usermnager : this is function created by microsoft will find all the functions you need to dell wil users 
        public async Task<IdentityResult> CreateAccount(SignUpModel signup)
        {
            ApplicaionUser applicaionUser = new ApplicaionUser();
            applicaionUser.UserName = signup.username;
            applicaionUser.Email = signup.email;
            applicaionUser.Name = signup.Name;
            applicaionUser.Bdate = signup.bdate;
            //save password without encryptiion 
            // applicaionUser.PasswordHash = signup.password;

            //code => insert aspnetuser
            //this is only recive application user object ; 
            var result = await userManager.CreateAsync(applicaionUser, signup.password);
            return result;

        }
        public async Task<SignInResult> signin(SignInModel signInModel)
        {

            var signinResult = await signIn.PasswordSignInAsync(signInModel.Username, signInModel.password, signInModel.Remember, false);
            return signinResult;
        }
        public async Task LogOut()
        {
            await signIn.SignOutAsync();
        }

        public async Task<IdentityResult> AddRole(RoleModel roleModel)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.Name;
            var result = await roleManager.CreateAsync(role);
            return result;
        }
        public List<ApplicaionUser> GetUsers()
        {

            List<ApplicaionUser> li = userManager.Users.ToList();
            return li;
        }
        //function to get roles for an employee selected 
        public async Task< List<UserRoles>> GetRoles(string user_id)
        {
            List<IdentityRole> Li = roleManager.Roles.ToList();

            List<UserRoles> liUrRole = new List<UserRoles>();
            foreach(IdentityRole item in Li)
            {
                UserRoles usr = new UserRoles()  ;
                usr.RoleId = item.Id;
                usr.NameRole = item.Name;
                usr.UserId = user_id;
                usr.IsSelected = false;
                liUrRole.Add(usr);
            }

            foreach (UserRoles UR in liUrRole)
            {
               var user = await userManager.FindByIdAsync(UR.UserId);
               var Role = await userManager.GetRolesAsync(user);
                foreach (string r in Role)
                {
                    if( r == UR.NameRole)
                    {
                        UR.IsSelected = true;
                    }
                }
            }
            return liUrRole;
        }

        public async Task UpdateUserRole(List<UserRoles> liUsrRole)
        {
            foreach(UserRoles item in liUsrRole)
            {
               var user = await userManager.FindByIdAsync(item.UserId);
                if (item.IsSelected == true)
                {//we want to check if the selected role is in table or not 
                    if (await userManager.IsInRoleAsync(user, item.NameRole) == false)
                    {
                        await userManager.AddToRoleAsync(user, item.NameRole);
                    }
                }
                else
                {
                    if (await userManager.IsInRoleAsync(user, item.NameRole) == true)
                    {
                        await userManager.RemoveFromRoleAsync(user, item.NameRole);
                    }
                }
                
            }
        }


        

    }
}
