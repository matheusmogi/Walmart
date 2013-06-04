using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Walmart.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Walmart.Data
{
    public class EstadoDAO : DataDAO
    {
        private static Func<IDataReader, Estado> Make = reader => new Estado()
        {
            Codigo = Convert.ToInt32(reader["Codigo"]),
            Nome = reader["nome"].ToString(),
            Pais = reader["Pais"].ToString(),
            Regiao = reader["Regiao"].ToString(),
            Sigla = reader["Sigla"].ToString()
        };
        public static void Novo(Estado estado)
        {
            try
            {
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "INSERT INTO ESTADO VALUES(@Pais,@Nome,@Sigla,@Regiao)";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@Pais", estado.Pais);
                    command.Parameters.AddWithValue("@Nome", estado.Nome);
                    command.Parameters.AddWithValue("@Sigla", estado.Sigla);
                    command.Parameters.AddWithValue("@Regiao", estado.Regiao);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static List<Estado> ObterEstados()
        {
            try
            {
                List<Estado> lst = new List<Estado>();
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "SELECT * FROM ESTADO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        lst.Add(Make(dr));
                    }
                    conn.Close();
                    return lst;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Estado ObterEstado(int codigo)
        {
            try
            {
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "SELECT * FROM ESTADO WHERE CODIGO = @CODIGO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@CODIGO", codigo);
                    SqlDataReader dr = command.ExecuteReader();
                    Estado estado = null;
                    if (dr.Read())
                        estado = Make(dr);
                    conn.Close();
                    return estado;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Atualizar(Estado estado)
        {
            try
            {
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "UPDATE ESTADO SET PAIS = @PAIS, NOME=@NOME, SIGLA=@SIGLA, REGIAO=@REGIAO WHERE CODIGO = @CODIGO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@Pais", estado.Pais);
                    command.Parameters.AddWithValue("@Nome", estado.Nome);
                    command.Parameters.AddWithValue("@Sigla", estado.Sigla);
                    command.Parameters.AddWithValue("@Regiao", estado.Regiao);
                    command.Parameters.AddWithValue("@Codigo", estado.Codigo);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Delete(int codigo)
        {
            if (CidadeDAO.ObterCidade(codEstado: codigo) != null)
            {
                throw new Exception("CIDADE");
            }
            try
            {
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "DELETE FROM ESTADO WHERE CODIGO = @CODIGO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@CODIGO", codigo);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
