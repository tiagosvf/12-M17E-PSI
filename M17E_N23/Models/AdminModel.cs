using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using M17E_N23;
using System.ComponentModel.DataAnnotations;

namespace M17E_N23.Models
{
    public class AdminModel
    {
        [Key]
        public int nr { get; set; }

         [Required(ErrorMessage = "Deve indicar o nome")]
        public string nome { get; set; }

     
        public string album { get; set; }
        [Required(ErrorMessage = "Deve escrever o comentário")]
        public string comentario { get; set; }
 
        public bool aprovado { get; set; }


    }
    public class AdminBD
    {

        public List<AdminModel> listaComentarios()
        {
            string sql = "SELECT Comentarios.id, Comentarios.comentario, Comentarios.nome AS [Nome comentador], Comentarios.aprovado, Albuns.nome FROM Comentarios INNER JOIN Albuns ON comentarios.album=albuns.id";
         
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<AdminModel> lista = new List<AdminModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AdminModel novo = new AdminModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.album = dados[4].ToString();
                novo.nome = dados[2].ToString();
                novo.aprovado = bool.Parse(dados[3].ToString());
                lista.Add(novo);
            }

            return lista;
        }

        //delete
        public void aprovarComentario(int id)
        {
            string sql = "UPDATE Comentarios SET aprovado=1 WHERE id = @id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            BD.Instance.executaComando(sql, parametros);
        }
        public void desaprovarComentario(int id)
        {
            string sql = "UPDATE Comentarios SET aprovado=0 WHERE id = @id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            BD.Instance.executaComando(sql, parametros);
        }

        public List<AdminModel> comentariosAlbum(int id)
        {
            string sql = "SELECT * FROM Comentarios WHERE album=@id AND aprovado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=id}
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<AdminModel> lista = new List<AdminModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AdminModel novo = new AdminModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.nome = dados[3].ToString();
                lista.Add(novo);
            }

            return lista;
        }
        public int adicionarComentario(AdminModel novo)
        {
            string sql = "INSERT INTO Comentarios(comentario,album,nome) VALUES";
            sql += " (@comentario,@album,@nome);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=novo.nome },
                 new SqlParameter() {ParameterName="@comentario",SqlDbType=SqlDbType.Text,Value=novo.comentario },
                  new SqlParameter() {ParameterName="@album",SqlDbType=SqlDbType.Int,Value=novo.nr },

            };
            BD.Instance.executaComando(sql, parametros);
            return novo.nr;
        }

        public List<AdminModel> pesquisa(string comentario)
        {
            string sql = "SELECT Comentarios.id, Comentarios.comentario, Comentarios.nome AS [Nome comentador], Comentarios.aprovado, Albuns.nome FROM Comentarios INNER JOIN Albuns ON comentarios.album=albuns.id WHERE comentario like @comentario";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@comentario",SqlDbType=System.Data.SqlDbType.VarChar,Value="%" + (string)comentario + "%" }
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<AdminModel> lista = new List<AdminModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AdminModel novo = new AdminModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.album = dados[4].ToString();
                novo.nome = dados[2].ToString();
                novo.aprovado = bool.Parse(dados[3].ToString());
                lista.Add(novo);
            }

            return lista;
        }
        public void removerComentario(int id)
        {
            string sql = "Delete From Comentarios WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            BD.Instance.executaComando(sql, parametros);
        }

        public List<AdminModel> lista()
        {
            string sql = "SELECT Comentarios.id, Comentarios.comentario, Comentarios.nome AS [Nome comentador], Comentarios.aprovado, Albuns.nome FROM Comentarios INNER JOIN Albuns ON comentarios.album=albuns.id";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<AdminModel> lista = new List<AdminModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AdminModel novo = new AdminModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.album = dados[4].ToString();
                novo.nome = dados[2].ToString();
                novo.aprovado = bool.Parse(dados[3].ToString());




                lista.Add(novo);
            }

            return lista;
        }
        public List<AdminModel> lista(int nr)
        {
            string sql = "SELECT Comentarios.id, Comentarios.comentario, Comentarios.nome AS [Nome comentador], Comentarios.aprovado, Albuns.nome FROM Comentarios INNER JOIN Albuns ON comentarios.album=albuns.id WHERE Comentarios.id=@nr";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=nr },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);

            List<AdminModel> lista = new List<AdminModel>();
            foreach (DataRow dados in registos.Rows)
            {
                AdminModel novo = new AdminModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.album = dados[4].ToString();
                novo.nome = dados[2].ToString();
                novo.aprovado = bool.Parse(dados[3].ToString());
                lista.Add(novo);
            }

            return lista;
        }


       
    }
}