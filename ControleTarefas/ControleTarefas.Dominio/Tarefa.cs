using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp
{
    public class Tarefa : EntidadeBase
    {
        public Tarefa(string titulo, int prioridade, DateTime dataCriacao, int percentualConcluido)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataCriacao = dataCriacao;
            this.dataConclusao = DateTime.MaxValue;
            this.percentualConcluido = percentualConcluido;
        }

        public string titulo { get; set; }

        public int prioridade { get; set; }

        public DateTime dataCriacao { get; set; }

        public DateTime dataConclusao { get; set; }

        public int percentualConcluido { get; set; }

        public override bool Validar()
        {
            if (ValidarPrioridade() && ValidarPercentualConcluido())
                return true;
            return false;
        }

        public bool ValidarPrioridade() 
        {
            if (prioridade == 1 || prioridade == 2 || prioridade == 3)
                return true;
            return false;
        }

        public bool ValidarPercentualConcluido() 
        {
            if ((percentualConcluido>-1) && (percentualConcluido<101))
                return true;
            return false;
        }
    }
}
