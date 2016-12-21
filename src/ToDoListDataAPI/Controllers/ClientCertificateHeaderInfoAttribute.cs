using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace ToDoListDataAPI.Controllers
{
    public class ClientCertificateHeaderInfoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var cert = actionExecutedContext.ActionContext.RequestContext.ClientCertificate;
            actionExecutedContext.Response.Content.Headers.Add("x-ClientCert-Thmubprint", cert != null ? cert.Thumbprint : "Cert is Null");
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}