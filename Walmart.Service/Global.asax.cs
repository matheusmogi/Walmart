using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace Walmart.Service
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.Add(new System.Web.Routing.Route("cidades/add/{*pathInfo}", new WebServiceRouteHandler("~/Adicionar.asmx")));
            routeCollection.Add(new System.Web.Routing.Route("cidades/{codCidade}", new WebServiceRouteHandler("~/Atualizar.asmx")));
            routeCollection.Add(new System.Web.Routing.Route("dcidades/{codCidade}", new WebServiceRouteHandler("~/Excluir.asmx")));
            routeCollection.Add(new System.Web.Routing.Route("scidades/{codCidade}", new WebServiceRouteHandler("~/Selecionar.asmx")));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}