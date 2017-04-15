using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using M17E_N23;

namespace M17E_N23.Models
{
    public class UtilizadoresModel
    {
        [Key]
       
        [Required(ErrorMessage = "Campo nome tem de ser preenchido")]
        [StringLength(50)]
        [MinLength(2, ErrorMessage = "Nome muito pequeno")]
        public string nome { get; set; }
        [Display(Name = "Palavra passe")]
        [MinLength(5, ErrorMessage = "Palavra passe muito pequena")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a sua palavra passe")]
        [Compare("password", ErrorMessage = "Palavras passe não são iguais")]
        public string confirmaPassword { get; set; }
        public int perfil { get; set; }
        public string email { get; set; }

        public bool estado { get; set; }
    }

    public class UtilizadoresBD
    {
        //create
        public void adicionarUtilizadores(UtilizadoresModel novo)
        {
            string sql = @"INSERT INTO Utilizadores(nome,email,password,perfil,estado)
                        VALUES (@nome,@email,HASHBYTES('SHA2_512',@password),@perfil,@estado)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=novo.nome},
                 new SqlParameter(){ParameterName="@email",
                    SqlDbType =SqlDbType.VarChar,Value=novo.email},
                new SqlParameter(){ParameterName="@password",
                    SqlDbType =SqlDbType.VarChar,Value=novo.password},
                 new SqlParameter(){ParameterName="@perfil",
                    SqlDbType =SqlDbType.Int,Value=novo.perfil},
                 new SqlParameter(){ParameterName="@estado",
                    SqlDbType =SqlDbType.Int,Value=novo.estado},
            };
            BD.Instance.executaComando(sql, parametros);
        }

    
        //read
        public List<UtilizadoresModel> lista()
        {
            string sql = "SELECT * FROM utilizadores";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();
            foreach (DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                
                novo.nome = data[0].ToString();
                novo.email = data[2].ToString();
                novo.perfil = int.Parse(data[1].ToString());
                novo.estado = bool.Parse(data[4].ToString());
                //novo.estado = bool.Parse(data[3].ToString());
                lista.Add(novo);
            }
            return lista;
        }
        public List<UtilizadoresModel> lista(string nome)
        {
            string sql = "SELECT * FROM utilizadores WHERE nome like @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value="%"+nome+"%"}
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();
            foreach (DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
              
                novo.nome = data[0].ToString();
                novo.email = data[2].ToString();
                novo.perfil = int.Parse(data[1].ToString());
                novo.estado = bool.Parse(data[4].ToString());
                lista.Add(novo);
            }
            return lista;
        }

        public List<UtilizadoresModel> listax(string nome)
        {
            string sql = "SELECT * FROM utilizadores WHERE nome=@nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=nome}
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();
            foreach (DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
                
                novo.nome = data[0].ToString();
                novo.email = data[2].ToString();
                novo.perfil = int.Parse(data[1].ToString());
                novo.estado = bool.Parse(data[4].ToString());
                lista.Add(novo);
            }
            return lista;
        }
        public List<UtilizadoresModel> pesquisa(string nome)
        {
            string sql = "SELECT * FROM Utilizadores WHERE nome like @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=System.Data.SqlDbType.VarChar,Value="%" + (string)nome + "%" }
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<UtilizadoresModel> lista = new List<UtilizadoresModel>();

            foreach (DataRow data in registos.Rows)
            {
                UtilizadoresModel novo = new UtilizadoresModel();
             
                novo.nome = data[0].ToString();
                novo.email = data[2].ToString();
                novo.perfil = int.Parse(data[1].ToString());
                novo.estado = bool.Parse(data[4].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        //update
        public void atualizarUtilizador(UtilizadoresModel novo)
        {
            string sql = @"UPDATE Utilizadores SET ";
            List<SqlParameter> parametros=null;
            if (!String.IsNullOrEmpty(novo.password))
            {
                sql += "password = HASHBYTES('SHA2_512', @password),";
                sql += "  perfil=@perfil,estado=@estado WHERE nome=@nome";
                parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=novo.nome},

                new SqlParameter() { ParameterName = "@password",
                    SqlDbType = SqlDbType.VarChar, Value = novo.password },

                 new SqlParameter(){ParameterName="@perfil",
                    SqlDbType =SqlDbType.Int,Value=novo.perfil},
                 new SqlParameter(){ParameterName="@estado",
                    SqlDbType =SqlDbType.Bit,Value=novo.estado},
            };
            }
            else
            {

                sql += "  perfil=@perfil,estado=@estado WHERE nome=@nome";
                parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=novo.nome},


                 new SqlParameter(){ParameterName="@perfil",
                    SqlDbType =SqlDbType.Int,Value=novo.perfil},
                 new SqlParameter(){ParameterName="@estado",
                    SqlDbType =SqlDbType.Bit,Value=novo.estado},
            };
            }
            BD.Instance.executaComando(sql, parametros);
        }
        //delete
        public void removerUtilizador(string nome)
        {
            string sql = "UPDATE Utilizadores SET estado=0 WHERE nome = @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =SqlDbType.VarChar,Value=nome}
            };
            BD.Instance.executaComando(sql, parametros);
        }
    }
}