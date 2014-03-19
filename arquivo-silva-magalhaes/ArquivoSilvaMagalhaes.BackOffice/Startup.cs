using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArquivoSilvaMagalhaes.BackOffice.Startup))]
namespace ArquivoSilvaMagalhaes.BackOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
