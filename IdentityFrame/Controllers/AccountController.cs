using IdentityFrame.Models;
using IdentityFrame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityFrame.Controllers
{
    public class AccountController : Controller
    {
       private readonly IAccount AccountServices;
        public AccountController(IAccount _AccountServices)
        {
            AccountServices = _AccountServices;
        }
        public IActionResult Index()
        {
            return View("CreateAccount");
        }
        public async Task<IActionResult> CreateAccount(SignUpModel signup)
        {
            //code
          var result=await AccountServices.CreateAccount(signup);
            return View("CreateAccount");
        }
        public IActionResult Signin()
        {
            return View("Signin");
        }
        public IActionResult Login()
        {
            return View("Signin");
        }
        public async Task< IActionResult> CheckPass(SignInModel sign)
        {
            //check username & password 
          var res= await AccountServices.signin(sign);
            if ( res.Succeeded == true)
            {

                return RedirectToAction("Index", "Dashboard");
               // return View("Signin");
            }
            else
            {
                ViewData["Message"] = "Invalid Username or Password";
                return View("Signin");
            }
           
        }
        public IActionResult LogOut()
        {
            //this is function to remove the value of the token 
            AccountServices.LogOut();
            return View("Signin");
        }
        //building pades for authorization 

      //  [Authorize(NewRole="Admin")]
        public IActionResult NewRole()
        {
            return View("NewRole");
        }
        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
           var result = await AccountServices.AddRole(roleModel);

            return View("NewRole");
        }
        public IActionResult GetUsers()
        {
           List<ApplicaionUser>li= AccountServices.GetUsers();

            return View("UserList" , li);
        }
        public async Task <IActionResult> UserRole(string Userid ,string name)
        {
            ViewData["Name"] = name;
           List<UserRoles>liUsrRole=await AccountServices.GetRoles(Userid);
            return View(liUsrRole);
        }
        public async Task<IActionResult> UpdateUserRole(List<UserRoles> li)
        {
           await AccountServices.UpdateUserRole(li);
            List<UserRoles> userRoles = await AccountServices.GetRoles(li[0].UserId);

            return View("UserRole", userRoles);
        }
     
    }

}
