using System.Web.Http;
using Swashbuckle.Application;
using WebActivatorEx;
using TodoList;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace TodoList
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "TodoList");
                    })
                    .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}