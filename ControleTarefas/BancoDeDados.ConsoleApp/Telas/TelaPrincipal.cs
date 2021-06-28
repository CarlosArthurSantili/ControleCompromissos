﻿using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp
{
    class TelaPrincipal : TelaBase
    {
        readonly TelaTarefa telaTarefa;
        readonly TelaContato telaContato;
        readonly TelaCompromisso telaCompromisso;
        readonly ControladorTarefa controladorTarefa;
        readonly ControladorContato controladorContato;
        readonly ControladorCompromisso controladorCompromisso;

        public TelaPrincipal()
        {
            controladorTarefa = new ControladorTarefa();
            controladorContato = new ControladorContato();
            controladorCompromisso = new ControladorCompromisso();

            telaTarefa = new TelaTarefa(controladorTarefa);
            telaContato = new TelaContato(controladorContato);
            telaCompromisso= new TelaCompromisso(controladorCompromisso, controladorContato);
        }
        
        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");
                Console.WriteLine("Digite 3 para o Cadastro de Compromissos");
                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaTarefa;
                else if (opcao == "2")
                    telaSelecionada = telaContato;
                else if (opcao == "3")
                    telaSelecionada = telaCompromisso;
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}
