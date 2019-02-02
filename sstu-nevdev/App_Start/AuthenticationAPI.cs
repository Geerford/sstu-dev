using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace sstu_nevdev.App_Start
{
    public class AuthenticationAPI : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent("You are unauthorized to access this resource")
            };
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = HttpContext.Current.User;
            string[] roles = Roles.Split(',');
            bool isInRole = false;
            foreach (string item in roles)
            {
                if (user.IsInRole(item))
                {
                    isInRole = true;
                }
            }
            if (!isInRole)
            {
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent("You have not admission to access this resource")
                };
            }
            base.OnAuthorization(actionContext);
        }
    }
}