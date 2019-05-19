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

        /// <summary>
        /// Gets User from Active Directory.
        /// </summary>
        /// <param name="identityValue">The identity value. <example>"Петр Петров", 
        /// "ivanov_ivan", etc</example>.</param>
        /// <param name="domain">The domain.<example>"aptech.com", "sstu.com", etc. 
        /// Can be used Environment.UserDomainName</example>.</param>
        /// <returns>The <see cref="IHttpActionResult"/> status code.</returns>
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

        /// <summary>
        /// Authorizes the user over Active Directory.
        /// </summary>
        /// <remarks>
        /// Generates security keys if needs.
        /// </remarks>
        /// <param name="model">The <see cref="AccountRequestAPIModel"/> object.</param>
        /// <returns>The <see cref="IHttpActionResult"/> status code.</returns>
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