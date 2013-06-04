using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Walmart.Model;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace Walmart.Data
{
    public class CidadeDAO : DataDAO
    {
        private static Func<IDataReader, Cidade> Make = reader => new Cidade()
        {
            Codigo = Convert.ToInt32(reader["Codigo"]),
            Nome = reader["nome"].ToString(),
            Capital = Convert.ToBoolean(reader["capital"]),
            Estado = EstadoDAO.ObterEstado(Convert.ToInt32(reader["codEstado"])),
            CodEstado = Convert.ToInt32(reader["codEstado"])
        };

        public static void Novo(Cidade cidade)
        {
            using (SqlConnection conn = DataDAO.Conexao())
            {
                SqlTransaction trans = conn.BeginTransaction("trans");
                try
                {
                    string commandStr = "CidadeInsert";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nome", cidade.Nome);
                    command.Parameters.AddWithValue("@Capital", cidade.Capital);
                    command.Parameters.AddWithValue("@CodEstado", cidade.CodEstado);
                    command.Transaction = trans;
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    cidade.Codigo = Convert.ToInt32(dt.Rows[0][0]);
                    cidade.Estado = EstadoDAO.ObterEstado(cidade.CodEstado);
                    using (add.Adicionar ws = new add.Adicionar())
                    {
                        ws.Url = "http://localhost:1000/Cidades/Add";
                        ws.Add(ConverterObjetoADD(cidade));
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
                conn.Close();
            }

        }

        private static add.Cidade ConverterObjetoADD(Cidade cidade)
        {
            add.Cidade c = new add.Cidade();
            c.Capital = cidade.Capital;
            c.Nome = cidade.Nome;
            c.Estado = new add.Estado();
            c.Estado.Codigo = cidade.Estado.Codigo;
            c.Estado.Nome = cidade.Estado.Nome;
            c.Estado.Pais = cidade.Estado.Pais;
            c.Estado.Regiao = cidade.Estado.Regiao;
            c.Estado.Sigla = cidade.Estado.Sigla;
            c.Codigo = cidade.Codigo;
            c.CodEstado = cidade.CodEstado;
            return c;
        }

        public static List<Cidade> ObterCidades()
        {
            try
            {
                List<Cidade> lst = new List<Cidade>();
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "SELECT * FROM CIDADE";
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

        public static Cidade ObterCidade(int? codigo = null, int? codEstado = null)
        {
            try
            {
                using (SqlConnection conn = DataDAO.Conexao())
                {
                    string commandStr = "SELECT * FROM CIDADE WHERE (@CODIGO IS NULL OR CODIGO = @CODIGO) AND (@CODESTADO IS NULL OR CODESTADO= @CODESTADO)";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@CODIGO", codigo.HasValue ? codigo.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CODESTADO", codEstado.HasValue ? codEstado.Value : (object)DBNull.Value);
                    SqlDataReader dr = command.ExecuteReader();
                    Cidade cidade = null;
                    if (dr.Read())
                        cidade = Make(dr);
                    conn.Close();
                    return cidade;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Atualizar(Cidade cidade)
        {
            using (SqlConnection conn = DataDAO.Conexao())
            {
                SqlTransaction trans = conn.BeginTransaction("trans");
                try
                {
                    string commandStr = "UPDATE CIDADE SET NOME=@NOME, CAPITAL=@CAPITAL, CODESTADO=@CODESTADO WHERE CODIGO = @CODIGO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@Nome", cidade.Nome);
                    command.Parameters.AddWithValue("@Codigo", cidade.Codigo);
                    command.Parameters.AddWithValue("@Capital", cidade.Capital);
                    command.Parameters.AddWithValue("@CodEstado", cidade.CodEstado);
                    command.Transaction = trans;
                    command.ExecuteNonQuery();
                    cidade.Estado = EstadoDAO.ObterEstado(cidade.CodEstado);
                    using (upd.Atualizar ws = new upd.Atualizar())
                    {
                        ws.Url = "http://localhost:1000/Cidades/" + cidade.Codigo.ToString();
                        ws.Update(ConverterObjetoUPD(cidade));
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();

                    throw;
                }
                conn.Close();
            }
        }

        private static upd.Cidade ConverterObjetoUPD(Cidade cidade)
        {
            upd.Cidade c = new upd.Cidade();
            c.Capital = cidade.Capital;
            c.Nome = cidade.Nome;
            c.Estado = new upd.Estado();
            c.Estado.Codigo = cidade.Estado.Codigo;
            c.Estado.Nome = cidade.Estado.Nome;
            c.Estado.Pais = cidade.Estado.Pais;
            c.Estado.Regiao = cidade.Estado.Regiao;
            c.Estado.Sigla = cidade.Estado.Sigla;
            c.Codigo = cidade.Codigo;
            c.CodEstado = cidade.CodEstado;
            return c;
        }

        public static void Delete(int codigo)
        {
            using (SqlConnection conn = DataDAO.Conexao())
            {
                SqlTransaction trans = conn.BeginTransaction("trans");
                try
                {
                    Cidade cidade = CidadeDAO.ObterCidade(codigo: codigo);
                    string commandStr = "DELETE FROM CIDADE WHERE CODIGO = @CODIGO";
                    SqlCommand command = new SqlCommand(commandStr, conn);
                    command.Parameters.AddWithValue("@CODIGO", codigo);
                    command.Transaction = trans;
                    command.ExecuteNonQuery();
                    using (del.Excluir ws = new del.Excluir())
                    {
                        ws.Url = "http://localhost:1000/dCidades/" + cidade.Codigo.ToString();
                        ws.Delete();
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                conn.Close();
            }
        }

        public static Cidade ObterCidadeXml(int codigo)
        {
            Cidade cidade = new Cidade();
            using (sel.Selecionar ws = new sel.Selecionar())
            {
                ws.Url = "http://localhost:1000/sCidades/" + codigo.ToString();
                cidade = ConvertWSLocal(ws.Select());
            }
            return cidade;
        }

        private static Cidade ConvertWSLocal(sel.Cidade cidade)
        {
            Cidade c = new Cidade();
            c.Capital = cidade.Capital;
            c.Nome = cidade.Nome;
            c.Estado = new Estado();
            c.Estado.Codigo = cidade.Estado.Codigo;
            c.Estado.Nome = cidade.Estado.Nome;
            c.Estado.Pais = cidade.Estado.Pais;
            c.Estado.Regiao = cidade.Estado.Regiao;
            c.Estado.Sigla = cidade.Estado.Sigla;
            c.Codigo = cidade.Codigo;
            c.CodEstado = cidade.CodEstado;
            return c;
        }
    }
}
