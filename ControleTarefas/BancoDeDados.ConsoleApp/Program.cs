using BancoDeDados.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (telaSelecionada is TelaTarefa)
                {
                    TelaTarefa telaTarefa = (TelaTarefa)telaSelecionada;

                    if (opcao == "1")
                        telaTarefa.InserirNovoRegistro();

                    else if (opcao == "2")
                        telaTarefa.VisualizarTarefas();

                    else if (opcao == "3")
                        telaTarefa.EditarRegistro();

                    else if (opcao == "4")
                        telaTarefa.ExcluirRegistro();
                }
                else if (telaSelecionada is TelaCompromisso)
                {
                    TelaCompromisso telaCompromisso = (TelaCompromisso)telaSelecionada;

                    if (opcao == "1")
                        telaCompromisso.InserirNovoRegistro();

                    else if (opcao == "2")
                        telaCompromisso.VisualizarCompromissos();

                    else if (opcao == "3")
                        telaCompromisso.EditarRegistro();

                    else if (opcao == "4")
                        telaCompromisso.ExcluirRegistro();
                }
                else if (telaSelecionada is ICadastravel)
                {
                    ICadastravel tela = (ICadastravel)telaSelecionada;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2") 
                    {
                        Console.Clear();
                        tela.VisualizarRegistros();
                        Console.ReadLine();
                    }
                       

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();
                }
                Console.Clear();
            }
        }
    }
}
