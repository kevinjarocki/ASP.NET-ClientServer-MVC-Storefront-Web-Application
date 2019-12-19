using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CaseStudy.Models;
using CaseStudy.Utils;
namespace CaseStudy.Controllers
{
    public class LoginController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInMgr;
        public LoginController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = userManager;
            _signInMgr = signInManager;
        }
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl = null)
        {
            if (HttpContext.Session.Get(SessionVars.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            }
            if (Convert.ToString(HttpContext.Session.Get(SessionVars.LoginStatus)) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVars.Message, "Don't forget to login before you shop!");
            }
            ViewBag.status = HttpContext.Session.GetString(SessionVars.LoginStatus);
            ViewBag.message = HttpContext.Session.GetString(SessionVars.Message);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure:true
                var result = await _signInMgr.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString(SessionVars.User, model.Email);
                    HttpContext.Session.SetString(SessionVars.LoginStatus, "logged in as " + model.Email);
                    HttpContext.Session.SetString(SessionVars.Message, "Welcome " + model.Email);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    HttpContext.Session.SetString(SessionVars.Message, "login attempt failed");
                    ViewBag.status = HttpContext.Session.GetString(SessionVars.LoginStatus);
                    ViewBag.message = HttpContext.Session.GetString(SessionVars.Message);
                    return View("Index");
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToLocal(returnUrl);
        }

        public async Task<IActionResult> Logoff()
        {
            await _signInMgr.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}