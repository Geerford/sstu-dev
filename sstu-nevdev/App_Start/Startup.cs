using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Service.Interfaces;
using System;
using static Services.Business.Service.IdentityService.Authentication;

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
                LoginPath = new PathString("/Account"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "SSTU_Security",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(6)
            });

            app.UseHangfireDashboard();
            //app.UseHangfireServer();
            RecurringJob.AddOrUpdate<IDatabaseService>(service => service.Sync(), Cron.Daily(2, 0));
            RecurringJob.AddOrUpdate<IDatabaseService>(service => service.Backup(), "0 1 * 4,8,12 *");
        }
    }
}