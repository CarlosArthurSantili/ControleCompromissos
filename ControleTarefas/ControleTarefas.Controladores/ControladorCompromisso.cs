using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Controladores
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        const string enderecoDBControle = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBControle;Integrated Security=True;Pooling=False";

        const string sqlSelecionarTodos = @"SELECT 
                [ID],
                [ASSUNTO],
                [LOCAL],
                [DATA],
                [HORAINICIO],
                [HORATERMINO],
                [LINK],
                [ID_CONTATO]
                FROM 
                TBCOMPROMISSO
                ORDER BY [DATA], [HORAINICIO] ASC;";

        const string sqlSelecionarPorId = @"SELECT 
                        [ID],
                        [ASSUNTO],
                        [LOCAL],
                        [DATA],
                        [HORAINICIO],
                        [HORATERMINO],
                        [LINK],
                        [ID_CONTATO]
                    FROM 
                        TBCOMPROMISSO
                    WHERE 
                        ID = @ID";

        const string sqlInserir = @"INSERT INTO TBCOMPROMISSO
                (
                [ASSUNTO],
                [LOCAL],
                [DATA],
                [HORAINICIO],
                [HORATERMINO],
                [LINK],
                [ID_CONTATO]
                )
            VALUES
                (
                @ASSUNTO,
                @LOCAL,
                @DATA,
                @HORAINICIO,
                @HORATERMINO,
                @LINK,
                @ID_CONTATO
                );" + @"SELECT SCOPE_IDENTITY();";

        const string sqlEditar = @"UPDATE TBCOMPROMISSO 
	                SET	
                        [ASSUNTO] = @ASSUNTO,
                        [LOCAL] = @LOCAL,
                        [DATA] = @DATA,
                        [HORAINICIO] = @HORAINICIO,
                        [HORATERMINO] = @HORATERMINO,
                        [LINK] = @LINK,
                        [ID_CONTATO] = @ID_CONTATO
	                WHERE 
		                [ID] = @ID;";

        const string sqlDeletar = @"DELETE FROM TBCOMPROMISSO 
                                    WHERE [ID] = @ID;";

        public List<Compromisso> ObterTodosCompromissos()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarTodos;

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            List<Compromisso> compromissos = new List<Compromisso>();

            while (leitorCompromissos.Read())
            {
                int id = Convert.ToInt32(leitorCompromissos["ID"]);
                string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromissos["LOCAL"]);
                DateTime data = Convert.ToDateTime(leitorCompromissos["DATA"]);
                DateTime horaInicio = Convert.ToDateTime(leitorCompromissos["HORAINICIO"]);
                DateTime horaTermino = Convert.ToDateTime(leitorCompromissos["HORATERMINO"]);
                string link = Convert.ToString(leitorCompromissos["LINK"]);
                
                int id_Contato;
                try
                {
                    id_Contato = Convert.ToInt32(leitorCompromissos["ID_CONTATO"]);
                }
                catch
                {
                    id_Contato = 0;
                }

                Compromisso Co = new Compromisso(assunto, local, data, horaInicio, horaTermino, link, id_Contato);
                Co.id = id;

                compromissos.Add(Co);
            }

            conexaoComBanco.Close();

            return compromissos;
        }

        public override bool InserirNovo(Compromisso compromisso)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            comandoInsercao.CommandText = sqlInserir;
            try
            {
                comandoInsercao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
                comandoInsercao.Parameters.AddWithValue("LOCAL", compromisso.local);
                comandoInsercao.Parameters.AddWithValue("DATA", compromisso.data);
                comandoInsercao.Parameters.AddWithValue("HORAINICIO", compromisso.horaInicio);
                comandoInsercao.Parameters.AddWithValue("HORATERMINO", compromisso.horaTermino);
                comandoInsercao.Parameters.AddWithValue("LINK", compromisso.link);
                comandoInsercao.Parameters.AddWithValue("ID_CONTATO", compromisso.id_Contato);

                object id = comandoInsercao.ExecuteScalar();

                compromisso.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InserirNovoCompromisso(Compromisso compromisso)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;

            comandoInsercao.CommandText = sqlInserir;
            try
            {
                comandoInsercao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
                comandoInsercao.Parameters.AddWithValue("LOCAL", compromisso.local);
                comandoInsercao.Parameters.AddWithValue("DATA", compromisso.data.Date);
                comandoInsercao.Parameters.AddWithValue("HORAINICIO", compromisso.horaInicio.TimeOfDay);
                comandoInsercao.Parameters.AddWithValue("HORATERMINO", compromisso.horaTermino.TimeOfDay);
                if (compromisso.link.Equals(""))
                    comandoInsercao.Parameters.AddWithValue("LINK", DBNull.Value);
                else
                    comandoInsercao.Parameters.AddWithValue("LINK", compromisso.link);

                if (compromisso.id_Contato == -1)
                    comandoInsercao.Parameters.AddWithValue("ID_CONTATO",DBNull.Value);
                else
                    comandoInsercao.Parameters.AddWithValue("ID_CONTATO", compromisso.id_Contato);

                object id = comandoInsercao.ExecuteScalar();

                compromisso.id = Convert.ToInt32(id);

                conexaoComBanco.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();

                return false;
            }
        }

        public override Compromisso SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            comandoSelecao.CommandText = sqlSelecionarPorId;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorCompromissos = comandoSelecao.ExecuteReader();

            if (leitorCompromissos.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorCompromissos["ID"]);
            string assunto = Convert.ToString(leitorCompromissos["ASSUNTO"]);
            string local = Convert.ToString(leitorCompromissos["LOCAL"]);
            DateTime data = Convert.ToDateTime(leitorCompromissos["DATA"]);
            DateTime horaInicio = Convert.ToDateTime(leitorCompromissos["HORAINICIO"]);
            DateTime horaTermino = Convert.ToDateTime(leitorCompromissos["HORATERMINO"]);
            
            string link;
            if (leitorCompromissos["LINK"] == DBNull.Value)
                link = "";
            else
                link = Convert.ToString(leitorCompromissos["LINK"]);


            int contato;
            if (leitorCompromissos["ID_CONTATO"] == DBNull.Value)
                contato = -1;
            else
                contato = Convert.ToInt32(leitorCompromissos["ID_CONTATO"]);


            Compromisso Co = new Compromisso(assunto, local, data, horaInicio, horaTermino, link, contato);
            Co.id = id;

            conexaoComBanco.Close();

            return Co;
        }

        public override bool EditarRegistro(int idSelecionado, Compromisso compromisso)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBControle;
            conexaoComBanco.Open();

            SqlCommand comandoAtualizacao = new SqlCommand();
            comandoAtualizacao.Connection = conexaoComBanco;

            comandoAtualizacao.CommandText = sqlEditar;

            
            comandoAtualizacao.Parameters.AddWithValue("ASSUNTO", compromisso.assunto);
            comandoAtualizacao.Parameters.AddWithValue("LOCAL", compromisso.local);
            comandoAtualizacao.Parameters.AddWithValue("DATA", compromisso.data);
            comandoAtualizacao.Parameters.AddWithValue("HORAINICIO", compromisso.horaInicio);
            comandoAtualizacao.Parameters.AddWithValue("HORATERMINO", compromisso.horaTermino);
            if (compromisso.link.Equals(""))
                comandoAtualizacao.Parameters.AddWithValue("LINK", DBNull.Value);
            else
                comandoAtualizacao.Parameters.AddWithValue("LINK", compromisso.link);

            if (compromisso.id_Contato == (-1))
                comandoAtualizacao.Parameters.AddWithValue("ID_CONTATO", DBNull.Value);
            else
                comandoAtualizacao.Parameters.AddWithValue("ID_CONTATO", compromisso.id_Contato);

            comandoAtualizacao.Parameters.AddWithValue("ID", idSelecionado);

            int linhasAfetadas = comandoAtualizacao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (linhasAfetadas > 0)
            {
                compromisso.id = idSelecionado;
                return true;
            }
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
