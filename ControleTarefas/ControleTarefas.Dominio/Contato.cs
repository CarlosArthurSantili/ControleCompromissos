using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Dominio
{
    public class Contato : EntidadeBase
    {
        public Contato(string nome, string email, long telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
            
        }
        
        public string nome { get; set; }

        public string email { get; set; }

        public long telefone { get; set; }

        public string empresa { get; set; }

        public string cargo { get; set; }

        
        public override bool Validar()
        {
            if (ValidarEmail() && ValidarTelefone())
                return true;
            return false;
        }

        public bool ValidarEmail()
        {
            if ((email.Contains("@")) && (email.Contains(".")))
                return true;
            return false;
        }

        public bool ValidarTelefone()
        {
            if ((telefone>99999999) && (telefone < 1000000000))
                return true;
            return false;
        }
    }
}
