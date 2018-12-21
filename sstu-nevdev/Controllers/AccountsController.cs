using Service.Interfaces;
using sstu_nevdev.Models;
using System.DirectoryServices.AccountManagement;
using System.Web.Http;

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
        public UserPrincipal Get(string identityValue, string domain)
        {
            return service.GetUser(identityValue, domain);
        }
        
        // POST api/accounts
        [HttpPost]
        public IHttpActionResult Post([FromBody]AccountModel model)
        {
            if(service.IsUserExist(model.User, model.Domain))
            {
                if(service.IsValidUser(model.User, model.Password))
                {
                    return Ok();
                }
                else
                {
                    return Json("Uncorrect username or password");
                }
            }
            else
            {
                return Json("Failed");
            }
        }
    }
}