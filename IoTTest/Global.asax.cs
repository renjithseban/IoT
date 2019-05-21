using Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IoTTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityWebApiActivator.Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
