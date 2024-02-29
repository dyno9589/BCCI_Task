using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json;

namespace BCCIAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private object globalconfiguration;

        protected void Application_Start()
        {

            //globalconfiguration.configuration.formatters.jsonformatter.serializersettings.referenceloophandling = newtonsoft.json.referenceloophandling.ignore;
            //globalconfiguration.configuration.formatters.remove(globalconfiguration.configuration.formatters.xmlformatter);




            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
