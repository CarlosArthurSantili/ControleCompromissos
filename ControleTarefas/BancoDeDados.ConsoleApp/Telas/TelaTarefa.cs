using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp
{
    public class TelaTarefa : TelaCadastros<Tarefa>, ICadastravel
    {
        readonly ControladorTarefa controladorTarefa;

        public TelaTarefa(ControladorTarefa controlador) : base("Cadastro de Caixas", controlador)
        {
            controladorTarefa = controlador;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova tarefa");
            Console.WriteLine("Digite 2 para visualizar tarefas");
            Console.WriteLine("Digite 3 para editar tarefas");
            Console.WriteLine("Digite 4 para excluir uma tarefa");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Tarefa ObterRegistro(TipoAcao acao)
        {
            Tarefa tarefa = new Tarefa("", 0, DateTime.MinValue, 0);
            do
            {
                Console.WriteLine(acao);
                Console.Write("Digite o título da tarefa: ");
                tarefa.titulo = Console.ReadLine();

                Console.Write("Digite a prioridade da tarefa: \n 1-Baixa\n 2-Média\n 3-Alta\n");
                tarefa.prioridade = Convert.ToInt32(Console.ReadLine());

                tarefa.dataCriacao = DateTime.Now;

                Console.Write("Digite o percentual concluido da tarefa:    (0% até 100%)\n");
                tarefa.percentualConcluido = Convert.ToInt32(Console.ReadLine());

               
                if (tarefa.percentualConcluido == 100)
                    tarefa.dataConclusao = DateTime.Now;
                else
                    tarefa.dataConclusao = DateTime.MaxValue;
                
                if (!tarefa.Validar())
                {
                    Console.WriteLine("Tarefa inválida, tente novamente");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (!tarefa.Validar());
            
            return tarefa;
        }

        public void VisualizarTarefas()
        {
            ConfigurarTela("Visualizando todas tarefas...");

            Console.WriteLine("Digite 1 para visualizar as tarefas concluídas...");
            Console.WriteLine("Digite 2 para visualizar as tarefas por fazer...");
            string opcao = Console.ReadLine();

            Console.Clear();

            if (opcao == "1")
            {
                if (EscreverTodasTarefasConcluidos())
                {
                    ApresentarMensagem("Sem registros concluídos...", TipoMensagem.Atencao);
                }
            }
            else if (opcao == "2")
            {
                if (EscreverTodasTarefasEmAberto())
                {
                    ApresentarMensagem("Sem registros em aberto...", TipoMensagem.Atencao);
                }
            }
            else           
                Console.WriteLine("Input errado!");            
        }

        public override void VisualizarRegistros()
        {
            if (controladorTarefa.ObterTodasTarefas().Count == 0)
            {
                Console.WriteLine("Não há tarefas cadastradas!");
                Console.Read();
                Console.Clear();
            }
            else
                foreach (Tarefa tarefa in controladorTarefa.ObterTodasTarefas())
                EscreverItensIndividualmente(tarefa);
        }

        private bool EscreverTodasTarefasConcluidos()
        {
            List<Tarefa> tarefas = controladorTarefa.ObterTodasTarefas();

            if (tarefas.Exists(x => x.percentualConcluido == 100))
            {
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 3 && item.percentualConcluido == 100)
                        EscreverItensIndividualmente(item);
                }
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 2 && item.percentualConcluido == 100)
                        EscreverItensIndividualmente(item);
                }
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 1 && item.percentualConcluido == 100)
                        EscreverItensIndividualmente(item);
                }
                Console.ReadLine();
                return false;
            }
            return true;
        }

        private bool EscreverTodasTarefasEmAberto()
        {
            List<Tarefa> tarefas = controladorTarefa.ObterTodasTarefas();

            if (tarefas.Exists(x => x.percentualConcluido != 100))
            {
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 3 && item.percentualConcluido != 100)
                        EscreverItensEmAbertoIndividualmente(item);
                }
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 2 && item.percentualConcluido != 100)
                        EscreverItensEmAbertoIndividualmente(item);
                }
                foreach (Tarefa item in tarefas)
                {
                    if (item.prioridade == 1 && item.percentualConcluido != 100)
                        EscreverItensEmAbertoIndividualmente(item);
                }
                Console.ReadLine();
                return false;
            }
            return true;
        }

        private static void EscreverItensEmAbertoIndividualmente(Tarefa item)
        {
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Titulo: " + item.titulo);
            Console.WriteLine("Prioridade: " + item.prioridade);
            Console.WriteLine("Data Criação: " + item.dataCriacao);
            Console.WriteLine("Percentual Concluido: " + item.percentualConcluido);
            Console.WriteLine();
        }

        private static void EscreverItensIndividualmente(Tarefa item)
        {
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Titulo: " + item.titulo);
            Console.WriteLine("Prioridade: " + item.prioridade);
            Console.WriteLine("Data Criação: " + item.dataCriacao);
            Console.WriteLine("Data Conclusão: " + item.dataConclusao);
            Console.WriteLine("Percentual Concluido: " + item.percentualConcluido);
            Console.WriteLine();
        }
    }
}