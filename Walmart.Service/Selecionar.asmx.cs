﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using Walmart.Model;
using System.IO;

namespace Walmart.Service
{
    /// <summary>
    /// Summary description for Selecionar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Selecionar : System.Web.Services.WebService
    {
        [WebMethod]
        [XmlInclude(typeof(Cidade))]
        public Cidade Select()
        {
            using (TextReader tw = new StreamReader(System.Web.HttpContext.Current.Server.MapPath
                    ("~/XML/" + HttpContext.Current.Request.RequestContext.RouteData.Values["codCidade"].ToString() + ".xml")))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Cidade));
                Cidade cidade = (Cidade)ser.Deserialize(tw);
                return cidade;
            }
        }
    }
}
