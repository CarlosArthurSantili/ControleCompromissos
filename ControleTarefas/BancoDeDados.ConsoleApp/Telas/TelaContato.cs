using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Telas
{
    public class TelaContato : TelaCadastros<Contato>, ICadastravel
    {
        readonly ControladorContato controladorContato;

        public TelaContato(ControladorContato controlador) : base("Tela Contato", controlador)
        {
            controladorContato = controlador;
        }

        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para inserir nova contato");
            Console.WriteLine("Digite 2 para visualizar contatos");
            Console.WriteLine("Digite 3 para editar contatos");
            Console.WriteLine("Digite 4 para excluir uma contato");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public override Contato ObterRegistro(TipoAcao acao)
        {
            Contato contato = new Contato("", "", 0, "", "");
            do
            {
                Console.WriteLine(acao);
                Console.Write("Digite o nome do contato: ");
                contato.nome = Console.ReadLine();

                Console.Write("Digite o email do contato: ");
                contato.email = Console.ReadLine();

                Console.Write("Digite o telefone do contato(9 digitos): ");
                contato.telefone = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite a empresa do contato: ");
                contato.empresa = Console.ReadLine();

                Console.Write("Digite o cargo do contato: ");
                contato.cargo = Console.ReadLine();

                if (!contato.Validar())
                {
                    Console.WriteLine("Contato inválido, tente novamente");
                    Console.ReadLine();
                }
                Console.Clear();
            } while (!contato.Validar());
            return contato;
        }

        public override void VisualizarRegistros()
        {
            if (controladorContato.ObterTodosContatos().Count == 0)
            {
                Console.WriteLine("Não há contatos cadastrados!");
                Console.Read();
                Console.Clear();
            }
            else
                foreach (Contato contato in controladorContato.ObterTodosContatos())
                EscreverItensIndividualmente(contato);
        }

        private static void EscreverItensIndividualmente(Contato item)
        {
            Console.WriteLine("ID: " + item.id);
            Console.WriteLine("Nome: " + item.nome);
            Console.WriteLine("Email: " + item.email);
            Console.WriteLine("Telefone: " + item.telefone);
            Console.WriteLine("Empresa: " + item.empresa);
            Console.WriteLine("Cargo: " + item.cargo);
            Console.WriteLine();
        }

    }
}
