using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Dominio
{
    public class Compromisso : EntidadeBase
    {
        public string assunto;

        public string local;

        public DateTime data;

        public DateTime horaInicio;

        public DateTime horaTermino;

        public string link = "";

        public int id_Contato = 0;

        public Compromisso(string assunto, string local, DateTime data, DateTime horaInicio, DateTime horaTermino, string link, int id_Contato)
        {
            this.assunto = assunto;
            this.local = local;
            this.data = data;
            this.horaInicio = horaInicio;
            this.horaTermino = horaTermino;
            this.link = link;
            this.id_Contato = id_Contato;
        }

        public override bool Validar()
        {
            if (ValidarData() && ValidarHorario())
                return true;
            return false;
        }

        public bool ValidarData()
        {
            if (data.CompareTo(DateTime.Now) <= 0)
                return false;
            return true;
        }

        public bool ValidarHorario()
        {
            if (horaTermino.TimeOfDay.CompareTo(horaInicio.TimeOfDay) <= 0) 
                return false;
            return true;
        }
    }
}
