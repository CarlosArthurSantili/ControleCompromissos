using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Telas
{
    public class TelaCompromisso : TelaCadastros<Compromisso>, ICadastravel
    {
        readonly ControladorCompromisso controladorCompromisso;
        readonly ControladorContato controladorContato;

        public TelaCompromisso(ControladorCompromisso controladorCompromisso, ControladorContato controladorContato) : base("Tela Compromisso", controladorCompromisso)
        {
            this.controladorCompromisso = controladorCompromisso;
            this.controladorContato = controladorContato;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir novo compromisso");
            Console.WriteLine("Digite 2 para visualizar compromissos");
            Console.WriteLine("Digite 3 para editar compromissos");
            Console.WriteLine("Digite 4 para excluir um compromisso");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Compromisso ObterRegistro(TipoAcao acao)
        {
            Compromisso compromisso = new Compromisso("", "", DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, "", 0);
            do
            {
                Console.WriteLine(acao);
                Console.Write("Digite o assunto do compromisso: ");
                compromisso.assunto = Console.ReadLine();

                Console.Write("Digite o local do compromisso: ");
                compromisso.local = Console.ReadLine();

                compromisso.data = ObterData();

                compromisso.horaInicio = ObterHorarioInicio();

                compromisso.horaTermino = ObterHorarioTermino();

                compromisso.link = ObterLink();

                compromisso.id_Contato = ObterIdContato();

                if (!compromisso.Validar())
                {
                    Console.WriteLine("Compromisso inválido, tente novamente");
                    Console.ReadLine();
                }
                else if (!ValidarConflitos(compromisso.data, compromisso.horaInicio, compromisso.horaTermino))
                {
                    Console.WriteLine("Houve conflito de horários, tente novamente");
                    Console.ReadLine();
                }
                Console.Clear();

            } while (!compromisso.Validar() || !ValidarConflitos(compromisso.data, compromisso.horaInicio, compromisso.horaTermino));
            return compromisso;
        }

        private static DateTime ObterData()
        {
            try 
            { 
                Console.Write("Digite a data do compromisso(dd/mm/aa): ");
                return Convert.ToDateTime(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Data inválida, tente novamente. " + e.Message);
                Console.ReadLine();
                ObterHorarioInicio();
            }
            return DateTime.MinValue;
        }

        private static DateTime ObterHorarioInicio()
        {
            try 
            {
                Console.Write("Digite o horário de inicío do compromisso(hh:mm:ss): ");
                return Convert.ToDateTime(Console.ReadLine());

            }
            catch (FormatException e)
            {
                Console.WriteLine("Horário de inicio inválido, tente novamente. " + e.Message);
                Console.ReadLine();
                ObterHorarioInicio();
            }
            return DateTime.MinValue;
        }

        private static DateTime ObterHorarioTermino()
        {
            try
            {
                Console.Write("Digite o horário de término do compromisso(hh:mm:ss): ");
                return Convert.ToDateTime(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Horário de termino inválido, tente novamente. "+e.Message);
                Console.ReadLine();
                ObterHorarioInicio();
            }
            return DateTime.MinValue;
        }

        public override void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo compromisso...");

            Compromisso registro = ObterRegistro(TipoAcao.Inserindo);

            if (controladorCompromisso.InserirNovoCompromisso(registro))
                Console.WriteLine("Registro inserido com sucesso!");
            else
                Console.WriteLine("Erro ao inserir registro");
            Console.ReadLine();
            Console.Clear();
        }

        private string ObterLink() 
        {
            string opcao;
            do
            {
                Console.WriteLine("Você deseja atribuir um link a esse compromisso? (s / n)");
                opcao = Console.ReadLine().ToUpper();
                Console.Clear();

                if (opcao == "S")
                {
                    Console.Write("Digite o link do compromisso: ");
                    string link = Console.ReadLine();
                    return link;
                }
                if (opcao == "N")
                {
                    return "";
                }

                Console.WriteLine("Opcao invalida, tente novamente");
                Console.ReadLine();
                Console.Clear();
            } while (true);
        }

        private int ObterIdContato()
        {
            string opcao;
            do
            {
                Console.WriteLine("Você deseja atribuir um contato a esse compromisso? (s / n)");
                opcao = Console.ReadLine().ToUpper();
                Console.Clear();

                if (opcao == "S")
                {
                    foreach (Contato contato in controladorContato.ObterTodosContatos())
                        Console.WriteLine("ID: "+contato.id+", Nome:"+contato.nome+ ", Empresa:" + contato.empresa + ", Empresa:" + contato.cargo);
                    Console.Write("Digite o id do contato que será atribuido ao compromisso: ");
                    int idContato = Convert.ToInt32(Console.ReadLine());

                    foreach (Contato contato in controladorContato.ObterTodosContatos())
                        if (contato.id == idContato)
                            return idContato;
                    Console.WriteLine("Não foi encontrado um contato com esse id, tente novamente");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (opcao == "N")
                {
                    return -1;
                }
                else
                {
                    Console.WriteLine("Opcao invalida, tente novamente");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (true);
        }

        public override void VisualizarRegistros() 
        {
            if (controladorCompromisso.ObterTodosCompromissos().Count == 0)
            {
                Console.WriteLine("Não há compromissos cadastrados!");
                Console.Read();
                Console.Clear();
            }
            else
            {
                foreach (Compromisso compromisso in controladorCompromisso.ObterTodosCompromissos())
                        EscreverItensIndividualmente(compromisso);
                Console.ReadLine();
            }
                
        }

        public void VisualizarCompromissos()
        {
            ConfigurarTela("Visualizando todas tarefas...");

            Console.WriteLine("Digite 1 para visualizar os compromissos até uma data determinada...");
            Console.WriteLine("Digite 2 para visualizar os compromissos passados...");
            Console.WriteLine("Digite 3 para visualizar TODOS os compromissos");
            string opcao = Console.ReadLine();

            Console.Clear();

            if (opcao == "1")
            {
                Console.WriteLine("Digite até que data deseja ver seus compromissos");
                DateTime dataEscolhida = Convert.ToDateTime(Console.ReadLine());
                Console.Clear();

                if (!EscreverTodosCompromissosEmAberto(dataEscolhida))
                {
                    ApresentarMensagem("Sem registros em aberto...", TipoMensagem.Atencao);
                }
                
            }
            else if (opcao == "2")
            {
                if (!EscreverTodosCompromissosConcluidos())
                {
                    ApresentarMensagem("Sem registros concluídos...", TipoMensagem.Atencao);
                }
            }
            else if (opcao == "3")
            {
                bool antigosCompromissos = EscreverTodosCompromissosConcluidos();
                bool atuaisCompromissos = EscreverTodosCompromissosEmAberto(DateTime.MaxValue);
                if (!antigosCompromissos && !atuaisCompromissos)
                {
                    ApresentarMensagem("Sem registros...", TipoMensagem.Atencao);
                }
            }
            else
                Console.WriteLine("Input errado!");
            Console.ReadLine();
        }

        private bool EscreverTodosCompromissosConcluidos()
        {
            List<Compromisso> compromissos = controladorCompromisso.ObterTodosCompromissos();
            if (compromissos.Exists(x => x.data >= DateTime.Now))
            {
                Console.WriteLine("~~~~~~~ Compromissos Antigos ~~~~~~~~");
                foreach (Compromisso compromisso in compromissos)
                {
                    if ((compromisso.data < DateTime.Now))
                        EscreverItensIndividualmente(compromisso);
                }
                Console.WriteLine();
                Console.WriteLine();
                return true;
            }
            return false;
        }

        private bool EscreverTodosCompromissosEmAberto(DateTime dataEscolhida)
        {
            List<Compromisso> compromissos = controladorCompromisso.ObterTodosCompromissos();

            if (compromissos.Exists(x => x.data.CompareTo(DateTime.Now)>=0))
            {
                Console.WriteLine("~~~~~~~~ Compromissos Atuais ~~~~~~~~");
                foreach (Compromisso compromisso in compromissos)
                {
                    if ((compromisso.data > DateTime.Now) && (compromisso.data < dataEscolhida))
                        EscreverItensIndividualmente(compromisso);
                }
                Console.WriteLine();
                Console.WriteLine();
                return true;
            }
            return false;
        }

        private void EscreverItensIndividualmente(Compromisso item)
        {
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Assunto: " + item.assunto);
            Console.WriteLine("Local: " + item.local);
            Console.WriteLine("Data: " + item.data.Date.ToShortDateString());
            Console.WriteLine("Hora Inicio: " + item.horaInicio.TimeOfDay);
            Console.WriteLine("Hora Termino: " + item.horaTermino.TimeOfDay);
            if (item.link == "")
            {
                Console.WriteLine("Link: null");
            }
            else
                Console.WriteLine("Link: " + item.link);

            if (item.id_Contato == 0)
            {
                Console.WriteLine("Contato: null");
            }
            else
                Console.WriteLine("Contato: " + controladorContato.SelecionarRegistroPorId(item.id_Contato).nome);
        }

        private bool ValidarConflitos(DateTime data, DateTime horaInicio, DateTime horaTermino)
        {
            foreach (Compromisso c in controladorCompromisso.ObterTodosCompromissos())
            {
                if (c.data.CompareTo(data) == 0)
                {
                    bool horaInicioInvalida = c.horaInicio.TimeOfDay.CompareTo(horaInicio.TimeOfDay) < 0 && c.horaTermino.TimeOfDay.CompareTo(horaInicio.TimeOfDay) > 0;
                    bool horaTerminoInvalida = c.horaInicio.TimeOfDay.CompareTo(horaTermino.TimeOfDay) < 0 && c.horaTermino.TimeOfDay.CompareTo(horaTermino.TimeOfDay) > 0;
                    bool horaInicioTerminoInvalida = c.horaInicio.TimeOfDay.CompareTo(horaInicio.TimeOfDay) > 0 && c.horaTermino.TimeOfDay.CompareTo(horaTermino.TimeOfDay) < 0;
                    if (horaInicioInvalida || horaTerminoInvalida || horaInicioTerminoInvalida)
                        return false;
                }
            }
                return true;
        }
    }
}
