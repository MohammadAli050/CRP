using EMSBankServiceApi;
using Swashbuckle.Application;
using System.Linq;
using System.Web.Http;
using WebActivatorEx;

namespace EMSBankServiceApi
{
    public class SwaggerConfig
    {

        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "EMSBankServiceApi");
                    // Add this line to resolve conflicts
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    // Optional: Include XML comments
                    // var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\bin\YourProjectName.xml";
                    // c.IncludeXmlComments(xmlPath);
                })
                .EnableSwaggerUi();
        }
    }
}
