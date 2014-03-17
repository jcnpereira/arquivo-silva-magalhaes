using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(arquivo_silva_magalhaes.Startup))]
namespace arquivo_silva_magalhaes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
