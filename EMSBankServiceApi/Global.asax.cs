using System.Web.Http;

namespace EMSBankServiceApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SwaggerConfig.Register(); // Add this line
        }
    }
}