using Microsoft.Owin.Security;
using Service.Interfaces;
using sstu_nevdev.Models;
using System.Web;
using System.Web.Http;
using static Services.Business.Services.IdentityService;

namespace sstu_nevdev.Controllers
{
    public class AccountsController : ApiController
    {
        IIdentityService service;

        public AccountsController(IIdentityService service)
        {
            this.service = service;
        }

        // GET api/accounts/identityValue=5&domain=aptech.com
        [HttpGet]
        public IHttpActionResult Get(string identityValue, string domain)
        {
            var user = service.GetUser(identityValue, domain);
            if(user != null)
            {
                return Json(user);
            }
            return BadRequest();
        }
        
        // POST api/accounts
        [HttpPost]
        public IHttpActionResult Post([FromBody]AccountAPIModel model)
        {
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var authService = new Authentication(authenticationManager);
            var authenticationResult = authService.SignIn(model.User + "@" + model.Domain, model.Password);
            if (authenticationResult.IsSuccess)
            {
                var user = service.GetUser(model.User, model.Domain);
                return Ok(user);
            }
            else
            {
                return Json(authenticationResult.ErrorMessage);
            }
        }
    }
}