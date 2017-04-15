using M17E_N23;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17E_N23.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nome de utilizador é obrigatório")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Palavra passe é obrigatória")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
    public class LoginBD
    {

        public UtilizadoresModel validarLogin(LoginModel login)
        {
            string sql = "SELECT * FROM utilizadores WHERE nome=@nome AND ";
            sql += " password=HASHBYTES('SHA2_512',@password) AND estado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=login.nome },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=login.password },
            };
            DataTable dados = BD.Instance.devolveConsulta(sql, parametros);
            UtilizadoresModel utilizador = null;

            if (dados != null && dados.Rows.Count > 0)
            {
                utilizador = new UtilizadoresModel();
                utilizador.nome = dados.Rows[0][0].ToString();
                utilizador.perfil = int.Parse(dados.Rows[0][1].ToString());
            }
            return utilizador;
        }
    }
}