using System.Web.Services;
using System.IO;
using Walmart.Model;
using System.Xml.Serialization;

namespace Walmart.Service
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Adicionar : System.Web.Services.WebService
    {
        [WebMethod]
        [XmlInclude(typeof(Cidade))]
        public void Add(Cidade cidade)
        {
            using (TextWriter tw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/XML/" + ((Cidade)cidade).Codigo.GetHashCode().ToString() + ".xml")))
            {
                XmlSerializer ser = new XmlSerializer(cidade.GetType(), new XmlRootAttribute("Cidade"));
                ser.Serialize(tw, cidade);
            }
        }

     
    }
}
