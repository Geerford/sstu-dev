using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using static Services.Business.Services.IdentityService.Authentication;

[assembly: OwinStartup(typeof(sstu_nevdev.App_Start.Startup))]
namespace sstu_nevdev.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = SSTUAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "SSTU_Security",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(6)
            });
        }
    }
}