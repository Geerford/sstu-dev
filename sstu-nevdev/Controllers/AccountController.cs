using Microsoft.Owin.Security;
using sstu_nevdev.Models;
using System.Web;
using System.Web.Mvc;
using static Services.Business.Service.IdentityService;
using static Services.Business.Service.IdentityService.Authentication;

namespace sstu_nevdev.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(AccountViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IAuthenticationManager authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var authService = new Authentication(authenticationManager);
                var authenticationResult = authService.SignIn(model.Username, model.Password);
                if (authenticationResult.IsSuccess)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", authenticationResult.ErrorMessage);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(SSTUAuthentication.ApplicationCookie);

            return RedirectToAction("Index", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}