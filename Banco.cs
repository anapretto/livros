using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livros
{
    class Banco
    {
        private static SQLiteConnection conexao;
        private static SQLiteConnection ConexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source=C:\\Users\\anaca\\Desktop\\Livros\\Banco de dados\\livros.db");
            conexao.Open();
            return conexao;
        }

        public static void NovoLivro(Novo l)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "INSERT INTO tb_livros (S_NOME, S_AUTOR, I_ANO) VALUES (@nome, @autor, @ano)";
                cmd.Parameters.AddWithValue("@nome", l.nome);
                cmd.Parameters.AddWithValue("@autor", l.autor);
                cmd.Parameters.AddWithValue("@ano", l.ano);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }

        public static DataTable Todos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM tb_livros";
                    da = new SQLiteDataAdapter(cmd.CommandText, ConexaoBanco());
                    da.Fill(dt);
                    ConexaoBanco().Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                ConexaoBanco().Close();
                throw ex;
            }
        }

        public static void AtualizarLivro(Novo l)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "UPDATE tb_livros Set S_NOME=@nome, S_AUTOR=@autor, I_ANO=@ano WHERE I_ID=@id";
                cmd.Parameters.AddWithValue("@nome", l.nome);
                cmd.Parameters.AddWithValue("@autor", l.autor);
                cmd.Parameters.AddWithValue("@ano", l.ano);
                cmd.Parameters.AddWithValue("@id", l.index);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }

        public static void DeletarLivro(Novo l)
        {
            try
            {
                var cmd = ConexaoBanco().CreateCommand();
                cmd.CommandText = "DELETE FROM tb_livros WHERE I_ID=@id";
                cmd.Parameters.AddWithValue("@id", l.index);
                cmd.ExecuteNonQuery();
                ConexaoBanco().Close();
            }
            catch (Exception ex)
            {
                throw ex;
                ConexaoBanco().Close();
            }
        }
    }
}
