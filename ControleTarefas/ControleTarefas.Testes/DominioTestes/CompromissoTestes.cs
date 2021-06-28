using BancoDeDados.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Testes.DominioTestes
{
    [TestClass]
    public class CompromissoTestes
    {
        [TestMethod]
        public void ValidarDataValida()
        {
            Compromisso teste = new Compromisso("assunto", "local", Convert.ToDateTime("10/10/2050"), Convert.ToDateTime("13:00"), Convert.ToDateTime("13:00"), "Link", 1 );

            Assert.AreEqual(true, teste.ValidarData());
        }

        [TestMethod]
        public void ValidarDataInvalida()
        {
            Compromisso teste = new Compromisso("assunto", "local", Convert.ToDateTime("10/10/2000"), Convert.ToDateTime("13:00"), Convert.ToDateTime("13:00"), "Link", 1);

            Assert.AreEqual(false, teste.ValidarData());
        }

        [TestMethod]
        public void ValidarHorarioValido()
        {
            Compromisso teste = new Compromisso("assunto", "local", Convert.ToDateTime("10/10/2000"), Convert.ToDateTime("13:00"), Convert.ToDateTime("14:00"), "Link", 1);

            Assert.AreEqual(true, teste.ValidarHorario());
        }

        [TestMethod]
        public void ValidarHorarioInvalido()
        {
            Compromisso teste = new Compromisso("assunto", "local", Convert.ToDateTime("10/10/2000"), Convert.ToDateTime("15:00"), Convert.ToDateTime("13:00"), "Link", 1);

            Assert.AreEqual(false, teste.ValidarHorario());
        }

    }
}
