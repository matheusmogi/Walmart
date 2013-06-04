using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Walmart.Data
{
    public class DataDAO
    {
        public static string ConnStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
                //return "Data Source="+(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))+"\\walmart.mdf;Persist Security Info=false;";
            }
        }
        public static SqlConnection Conexao()
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            conn.Open();
            return conn;
        }
    }
}
