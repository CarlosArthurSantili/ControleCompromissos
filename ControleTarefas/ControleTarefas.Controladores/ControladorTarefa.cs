using BancoDeDados.ConsoleApp.Controladores;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        const string enderecoDBControle = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBControle;Integrated Security=True;Pooling=False";
        
        const string sqlSelecionarTodos = @"SELECT 
                [ID],
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUSAO]
                FROM 
                TBTAREFA";

        const string sqlSelecionarPorId = @"SELECT 
                        [ID],
                        [TITULO],
                        [PRIORIDADE],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUSAO]
                    FROM 
                        TBTAREFA
                    WHERE 
                        ID = @ID";

        const string sqlInserir = @"INSERT INTO TBTAREFA
                (                
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUSAO]
                )
            VALUES
                (
                @TITULO,
                @PRIORIDADE,
                @DATACRIACAO,
                @DATACONCLUSAO,
                @PERCENTUALCONCLUSAO
                );" + @"SELECT SCOPE_IDENTITY();";

        const string sqlEditar = @"UPDATE TBTAREFA 
	                SET	
                        [TITULO] = @TITULO,
                        [PRIORIDADE] = @PRIORIDADE,
                        [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PERCENTUALCONCLUSAO] = @PERCENTUALCONCLUSAO
	                WHERE 
		                [ID] = @ID;";

        const string sqlDeletar = @"DELETE FROM TBTAREFA 
                                    WHERE [ID] = @ID;";

        public List<Tarefa> ObterTodasTarefas()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarTodos;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string nome = Convert.ToString(leitorTarefas["TITULO"]);
                int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConcluido = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUSAO"]);

                Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, percentualConcluido);
                Ta.dataConclusao = dataConclusao;
                Ta.id = id;

                tarefas.Add(Ta);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public override bool InserirNovo(Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            comandoInsercao.CommandText = sqlInserir;
            try
            {
                comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.titulo);
                comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
                comandoInsercao.Parameters.AddWithValue("DATACRIACAO", tarefa.dataCriacao);
                comandoInsercao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.dataConclusao);
                comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUSAO", tarefa.percentualConcluido);

                object id = comandoInsercao.ExecuteScalar();

                tarefa.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override Tarefa SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarPorId;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorTarefas["ID"]);
            string nome = Convert.ToString(leitorTarefas["TITULO"]);
            int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            int percentualConcluido = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);

            Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, percentualConcluido);
            Ta.dataConclusao = dataConclusao;
            Ta.id = id;

            conexaoComBanco.Close();

            return Ta;
        }

        public override bool EditarRegistro(int idSelecionado, Tarefa tarefa)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            comandoAtualizacao.CommandText = sqlEditar;

            comandoAtualizacao.Parameters.AddWithValue("TITULO", tarefa.titulo);
            comandoAtualizacao.Parameters.AddWithValue("PRIORIDADE", tarefa.prioridade);
            comandoAtualizacao.Parameters.AddWithValue("DATACONCLUSAO", tarefa.dataConclusao);
            comandoAtualizacao.Parameters.AddWithValue("PERCENTUALCONCLUSAO", tarefa.percentualConcluido);
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