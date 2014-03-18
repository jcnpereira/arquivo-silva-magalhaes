using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArquivoSilvaMagalhaes.Startup))]
namespace ArquivoSilvaMagalhaes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
