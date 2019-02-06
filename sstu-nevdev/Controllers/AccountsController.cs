using Microsoft.Owin.Security;
using Service.Interfaces;
using Services.Business.Security;
using sstu_nevdev.App_Start;
using sstu_nevdev.Models;
using System.Net;
using System.Web;
using System.Web.Http;
using static Services.Business.Service.IdentityService;

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
        [AuthenticationAPI(Roles = "SSTU_Deanery,SSTU_Administrator")]
        public IHttpActionResult Get(string identityValue, string domain)
        {
            var user = service.GetUser(identityValue, domain);
            if(user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }
        
        // POST api/accounts
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Post([FromBody]AccountRequestAPIModel model)
        {
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            var authService = new Authentication(authenticationManager);
            var authenticationResult = authService.SignIn(model.User + "@" + model.Domain, model.Password);
            if (authenticationResult.IsSuccess)
            {
                if (Keys.PublicKey == null)
                {
                    service.GenerateKey();
                }
                var response = (AccountResponseAPIModel)service.GetUser(model.User, model.Domain);
                response.GrasshopperKey = service.GetGrasshopperKey(model.PublicKey);
                return Ok(response);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, authenticationResult.ErrorMessage);
            }
        }
    }
}