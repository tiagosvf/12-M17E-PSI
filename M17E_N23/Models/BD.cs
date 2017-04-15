using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17E_N23
{
    public class BD
    {
        private static BD instance;
        public static BD Instance {
            get {
                if (instance == null)
                    instance = new BD();
                return instance;
            }
        }
        private string strLigacao;
        private SqlConnection ligacaoBD;
        public BD()
        {
            //ligação à bd
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }
        ~BD()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #region Funções genéricas
        //devolve consulta
        public DataTable devolveConsulta(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }
        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }
        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Transaction = transacao;
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }
        public bool executaComando(string sql)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool executaComando(string sql, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }
        public bool executaComando(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }
        public int executaScalar(string sql)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                valor = (int)comando.ExecuteScalar();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return valor;
            }
            return valor;
        }
        public int executaScalar(string sql, List<SqlParameter> parametros)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand(sql, ligacaoBD);
                comando.Parameters.AddRange(parametros.ToArray());
                valor = (int)comando.ExecuteScalar();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return valor;
            }
            return valor;
        }
        #endregion
    }
}