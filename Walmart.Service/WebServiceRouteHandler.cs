using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Services.Protocols;
using System.Web;

namespace Walmart.Service
{
    public class WebServiceRouteHandler : IRouteHandler
    {
        private readonly string _virtualPath;
        private readonly WebServiceHandlerFactory _handlerFactory = new WebServiceHandlerFactory();
        public WebServiceRouteHandler(string virtualPath)
        {
            if (virtualPath == null)
                throw new ArgumentNullException("virtualPath");
            if (!virtualPath.StartsWith("~/"))
                throw new ArgumentException("O caminho virtual deve começar com ~/", "virtualPath");
            _virtualPath = virtualPath;
        }
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return _handlerFactory.GetHandler(HttpContext.Current, requestContext.HttpContext.Request.HttpMethod, _virtualPath, requestContext.HttpContext.Server.MapPath(_virtualPath));
        }
    }
}
