using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using System.IO;
using Walmart.Model;

namespace Walmart.Service
{
    /// <summary>
    /// Summary description for Update
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Atualizar : System.Web.Services.WebService
    {
        [WebMethod]
        [XmlInclude(typeof(Cidade))]
        public void Update(Cidade cidade)
        {
            using (TextWriter tw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath
                    ("~/XML/" + HttpContext.Current.Request.RequestContext.RouteData.Values["codCidade"].ToString() + ".xml")))
            {
                XmlSerializer ser = new XmlSerializer(cidade.GetType(), new XmlRootAttribute("Cidade"));
                ser.Serialize(tw, cidade);
            }
        }
    }
}
