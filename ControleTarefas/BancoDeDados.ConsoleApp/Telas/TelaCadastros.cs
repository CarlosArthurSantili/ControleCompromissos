using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Telas
{
    public abstract class TelaCadastros<T> : TelaBase where T : EntidadeBase
    {

        protected Controlador<T> controlador;
       

        public TelaCadastros(string titulo, Controlador<T> controlador)
        {
            this.controlador = controlador;
        }

        public virtual void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo registro...");

            T registro = ObterRegistro(TipoAcao.Inserindo);

            if (controlador.InserirNovo(registro))
                Console.WriteLine("Registro inserido com sucesso!");
            else
                Console.WriteLine("Erro ao inserir registro");
            Console.ReadLine();
            Console.Clear();
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando registro...");

            VisualizarRegistros();

            Console.WriteLine("Digite o ID do registro que deseja editar...");
            int idPesquisado = Convert.ToInt32(Console.ReadLine());

            T registroNovo = ObterRegistro(TipoAcao.Editando);

            
            if (controlador.EditarRegistro(idPesquisado, registroNovo))
                Console.WriteLine("Registro editado com sucesso!");
            else
                Console.WriteLine("Erro ao editar o registro");
            Console.ReadLine();
            Console.Clear();
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Exclusão de registros...");

            VisualizarRegistros();

            Console.WriteLine("Digite o ID do registro que deseja excluir...");
            int idPesquisado = Convert.ToInt32(Console.ReadLine());

            if (controlador.ExcluirRegistro(idPesquisado))
                Console.WriteLine("Registro excluido com sucesso!");
            else
                Console.WriteLine("Erro ao excluir o registro");
            Console.ReadLine();
            Console.Clear();
        }


        public abstract void VisualizarRegistros();

        public abstract T ObterRegistro(TipoAcao acao);

    }
}
