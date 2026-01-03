using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Managers.Concretes;
using Project.Common.Tools;
using Project.Entities.Models;
using Project.MvcUI.Models.PureVms.AppUsers;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Project.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //Identity'nin UserManager sistemi kullanıcı ekleme silme güncelleme ve raporlama (CRUD) ile ilgilenir...
        private readonly SignInManager<AppUser> _signInManager; //Identity'nin SignInManager sistemi kullanıcı login durumuyla ilgilenir...
        private readonly RoleManager<AppRole> _roleManager; //Identity'nin RoleManager sistemi role yapısı icin crud ile ilgilenir...
        private readonly IAppUserRoleManager _appUserRoleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IAppUserRoleManager appUserRoleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appUserRoleManager = appUserRoleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel item)
        {
            Guid specId = Guid.NewGuid(); //unique kod yarattık

            AppUser appUser = new()
            {
                UserName = item.UserName,
                Email = item.Email,
                ActivationCode = specId
            };

            IdentityResult result = await _userManager.CreateAsync(appUser,item.Password);

            if (result.Succeeded) 
            {
                #region RolKontrolIslemleri

                if (!await _roleManager.RoleExistsAsync("Member")) await _roleManager.CreateAsync(new() { Name = "Member" });

                AppRole appRole = await _roleManager.FindByNameAsync("Member");

                AppUserRole appUserRole = new AppUserRole()
                {
                    RoleId = appRole.Id,
                    UserId = appUser.Id
                };

                await _appUserRoleManager.CreateAsync(appUserRole);
                #endregion

                string message = $"Hesabınız olusturulmustur...Üyeliginizi onaylamak icin lütfen http://localhost:5146/Home/ConfirmEmail?specId={specId}&id={appUser.Id} linkine tıklayınız";
                MailSender.Send(item.Email, body: message);

                TempData["Message"] = "Lütfen hesabınızı onaylamak icin emailinizi kontrol ediniz";
                return RedirectToAction("RedirectPanel");
                
            }

            return View();
        }


        public async Task<IActionResult> ConfirmEmail(Guid specId,int id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null) 
            {
                TempData["Message"] = "Kullanıcı bulunamadı";
                return RedirectToAction("RedirectPanel");
            }
            else if(appUser.ActivationCode == specId)
            {
                appUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(appUser);
                TempData["Message"] = "Hesabınız basarıyla onaylandı";
                return RedirectToAction("SignIn");
            }

            return RedirectToAction("Register");
        }

        public IActionResult RedirectPanel()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserRegisterRequestModel model)
        {
            AppUser appUser = await _userManager.FindByNameAsync(model.UserName);

            SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, true, true);

            if (result.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(appUser);
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Category", new { Area = "Admin" });
                }
                else if (roles.Contains("Member"))
                {
                    return RedirectToAction("Index"); //todo : Shopping Controller'i olarak degiştirilecek
                }

                return RedirectToAction("Index");
            }
            else if (result.IsNotAllowed)
            {
                return RedirectToAction("MailPanel");
            }
            TempData["Message"] = "Kullanıcı bulunamadı";

            return View();
        }

        public IActionResult MailPanel()
        {
            return View();
        }
    }
}
