using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Controladores
{
    public class ControladorContato : Controlador<Contato>
    {
        const string enderecoDBControle = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBControle;Integrated Security=True;Pooling=False";

        const string sqlSelecionarTodos = @"SELECT 
                [ID],
                [NOME],
                [EMAIL],
                [TELEFONE],
                [EMPRESA],
                [CARGO]
                FROM 
                TBCONTATO
                ORDER BY [CARGO];";

        const string sqlSelecionarPorId = @"SELECT 
                        [ID],
                        [NOME],
                        [EMAIL],
                        [TELEFONE],
                        [EMPRESA],
                        [CARGO]
                    FROM 
                        TBCONTATO
                    WHERE 
                        ID = @ID";

        const string sqlInserir = @"INSERT INTO TBCONTATO
                (                
                [NOME],
                [EMAIL],
                [TELEFONE],
                [EMPRESA],
                [CARGO]
                )
            VALUES
                (
                @NOME,
                @EMAIL,
                @TELEFONE,
                @EMPRESA,
                @CARGO
                );" + @"SELECT SCOPE_IDENTITY();";

        const string sqlEditar = @"UPDATE TBCONTATO 
	                SET	
                        [NOME] = @NOME,
                        [EMAIL] = @EMAIL,
                        [TELEFONE] = @TELEFONE,
                        [EMPRESA] = @EMPRESA,
                        [CARGO] = @CARGO
	                WHERE 
		                [ID] = @ID;";

        const string sqlDeletar = @"DELETE FROM TBCONTATO 
                                    WHERE [ID] = @ID;";

        public List<Contato> ObterTodosContatos()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarTodos;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                int telefone = Convert.ToInt32(leitorContatos["TELEFONE"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato Ta = new Contato(nome, email, telefone, empresa, cargo);
                Ta.id = id;

                contatos.Add(Ta);
            }

            conexaoComBanco.Close();

            return contatos;
        }

        public override bool InserirNovo(Contato contato)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            comandoInsercao.CommandText = sqlInserir;
            try
            {
                comandoInsercao.Parameters.AddWithValue("NOME", contato.nome);
                comandoInsercao.Parameters.AddWithValue("EMAIL", contato.email);
                comandoInsercao.Parameters.AddWithValue("TELEFONE", contato.telefone);
                comandoInsercao.Parameters.AddWithValue("EMPRESA", contato.empresa);
                comandoInsercao.Parameters.AddWithValue("CARGO", contato.cargo);

                object id = comandoInsercao.ExecuteScalar();

                contato.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override Contato SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarPorId;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            if (leitorContatos.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            int telefone = Convert.ToInt32(leitorContatos["TELEFONE"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);

            Contato Ta = new Contato(nome, email, telefone, empresa, cargo);
            Ta.id = id;

            conexaoComBanco.Close();

            return Ta;
        }

        public override bool EditarRegistro(int idSelecionado, Contato contato)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            comandoAtualizacao.CommandText = sqlEditar;

            comandoAtualizacao.Parameters.AddWithValue("NOME", contato.nome);
            comandoAtualizacao.Parameters.AddWithValue("EMAIL", contato.email);
            comandoAtualizacao.Parameters.AddWithValue("TELEFONE", contato.telefone);
            comandoAtualizacao.Parameters.AddWithValue("EMPRESA", contato.empresa);
            comandoAtualizacao.Parameters.AddWithValue("CARGO", contato.cargo);
            comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);

            int linhasAfetadas = comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (linhasAfetadas > 0)
                return true;
            return false;
        }

        public override bool ExcluirRegistro(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoExcluir = new SqlCommand();
            comandoExcluir.Connection = conexaoComBanco;

            comandoExcluir.CommandText = sqlDeletar;

            comandoExcluir.Parameters.AddWithValue("ID", id);

            int linhasAfetadas = comandoExcluir.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (linhasAfetadas > 0)
                return true;
            return false;
        }
    }
}
