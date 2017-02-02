using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvoiceMEF.Startup))]
namespace InvoiceMEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
