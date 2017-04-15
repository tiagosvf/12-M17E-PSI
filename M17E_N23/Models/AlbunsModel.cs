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
    public class AlbunsModel
    {
        [Key]
        public int nr { get; set; }

        [Required(ErrorMessage = "Deve indicar o noe")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Deve indicar o ano")]
        public int ano { get; set; }

        [Required(ErrorMessage = "Deve preencher a review")]
        public string review { get; set; }

        [Required(ErrorMessage = "Deve indicar o escritor")]
        public string escritor { get; set; }
        [Required(ErrorMessage = "Deve indicar o artista")]
        public string artista { get; set; }
        [Required(ErrorMessage = "Deve indicar a classficação")]
        public float classificacao { get; set; }
        //[Required(ErrorMessage = "Deve indicar o comentário")]

        public string comentario { get; set; }
        public string nome_comentador { get; set; }


    }
    public class AlbunsBD
    {
        public void atualizarAlbum(AlbunsModel album)
        {
            string sql = "UPDATE Albuns SET nome=@nome,artista=@artista,ano=@ano,classificacao=@classificacao,review=@review WHERE id=@id";
           List<SqlParameter> parametros = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=album.nr },
              new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=album.nome },
                new SqlParameter() {ParameterName="@artista",SqlDbType=SqlDbType.VarChar,Value=album.artista },
                new SqlParameter() {ParameterName="@ano",SqlDbType=SqlDbType.Int,Value=album.ano },
                new SqlParameter() {ParameterName="@classificacao",SqlDbType=SqlDbType.Float,Value=album.classificacao },
                 new SqlParameter() {ParameterName="@review",SqlDbType=SqlDbType.Text,Value=album.review },
            };
            BD.Instance.executaComando(sql, parametros);
            return;
        }

        public void apagarAlbum(int id)
        {

            string sql = "DELETE FROM Albuns WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=id }
            };
            BD.Instance.executaComando(sql, parametros);
        }

        public int adicionarAlbuns(AlbunsModel novo)
        {
            string sql = "INSERT INTO Albuns(nome,artista,ano,classificacao,review,escritor) VALUES";
            sql += " (@nome,@artista,@ano,@classificacao,@review,@escritor)SELECT cast(scope_identity() as int);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=novo.nome },
                new SqlParameter() {ParameterName="@artista",SqlDbType=SqlDbType.VarChar,Value=novo.artista },
                new SqlParameter() {ParameterName="@ano",SqlDbType=SqlDbType.Int,Value=novo.ano },
                new SqlParameter() {ParameterName="@classificacao",SqlDbType=SqlDbType.Float,Value=novo.classificacao },
                 new SqlParameter() {ParameterName="@review",SqlDbType=SqlDbType.Text,Value=novo.review },
                   new SqlParameter() {ParameterName="@escritor",SqlDbType=SqlDbType.Text,Value=novo.escritor },

            };
            int id = (int)BD.Instance.executaScalar(sql, parametros);
            return id;
        }
        public List<AlbunsModel> comentariosAlbum(int id)
        {
            string sql = "SELECT * FROM Comentarios WHERE album=@id AND aprovado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=System.Data.SqlDbType.Int,Value=id}
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<AlbunsModel> lista = new List<AlbunsModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.comentario = dados[1].ToString();
                novo.nome = dados[3].ToString();
                lista.Add(novo);
            }

            return lista;
        }

        public List<AlbunsModel> topEscritores()
        {
            string sql = "SELECT Count(nome) AS Reviews,escritor FROM Albuns GROUP BY escritor ORDER BY Reviews";
          
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<AlbunsModel> lista = new List<AlbunsModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
                novo.nr = int.Parse(dados[0].ToString());
            
                novo.nome = dados[1].ToString();
                lista.Add(novo);
            }

            return lista;
        }
        public int adicionarComentario(AlbunsModel novo)
        {
            string sql = "INSERT INTO Comentarios(comentario,album,nome) VALUES";
            sql += " (@comentario,@album,@nome);";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=novo.nome_comentador },
                 new SqlParameter() {ParameterName="@comentario",SqlDbType=SqlDbType.Text,Value=novo.comentario },
                  new SqlParameter() {ParameterName="@album",SqlDbType=SqlDbType.Int,Value=novo.nr },

            };
            BD.Instance.executaComando(sql, parametros);
            return novo.nr;
        }

        public List<AlbunsModel> pesquisa(string nome)
        {
            string sql = "SELECT * FROM Albuns WHERE nome like @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome",SqlDbType=System.Data.SqlDbType.VarChar,Value="%" + (string)nome + "%" }
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);
            List<AlbunsModel> lista = new List<AlbunsModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.review = dados[2].ToString();
                novo.artista = dados[3].ToString();
                novo.ano = int.Parse(dados[4].ToString());
                novo.classificacao = float.Parse(dados[5].ToString());
                novo.escritor = dados[7].ToString();
                lista.Add(novo);
            }

            return lista;
        }

        public List<AlbunsModel> lista()
        {
            string sql = "SELECT id, Nome, Review, Artista, Ano, Classificacao,escritor FROM Albuns";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<AlbunsModel> lista = new List<AlbunsModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.review = dados[2].ToString();
                novo.artista = dados[3].ToString();
                novo.ano = int.Parse(dados[4].ToString());
                novo.classificacao = float.Parse(dados[5].ToString());
                novo.escritor = dados[6].ToString();


                lista.Add(novo);
            }

            return lista;
        }

        public List<AlbunsModel> listaEscritores()
        {
            string sql = "SELECT nome From Utilizadores WHERE perfil=2";
            DataTable registos = BD.Instance.devolveConsulta(sql);
            List<AlbunsModel> lista = new List<AlbunsModel>();

            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
               
                novo.nome = dados[0].ToString();
               


                lista.Add(novo);
            }

            return lista;
        }
        public List<AlbunsModel> lista(int nr)
        {
            string sql = "SELECT id, Nome, Review, Artista, Ano, Classificacao,escritor FROM Albuns WHERE id=@nr";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nr",SqlDbType=SqlDbType.Int,Value=nr },
            };
            DataTable registos = BD.Instance.devolveConsulta(sql, parametros);

            List<AlbunsModel> lista = new List<AlbunsModel>();
            foreach (DataRow dados in registos.Rows)
            {
                AlbunsModel novo = new AlbunsModel();
                novo.nr = int.Parse(dados[0].ToString());
                novo.nome = dados[1].ToString();
                novo.review = dados[2].ToString();
                novo.artista = dados[3].ToString();
                novo.ano = int.Parse(dados[4].ToString());
                novo.classificacao = float.Parse(dados[5].ToString());
                novo.nome_comentador = "";
                novo.comentario = "";
                novo.escritor = dados[7].ToString();


                lista.Add(novo);
            }

            return lista;
        }
      
        
  
    }
}