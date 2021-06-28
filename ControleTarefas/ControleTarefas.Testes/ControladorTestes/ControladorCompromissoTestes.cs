using BancoDeDados.ConsoleApp.Controladores;
using BancoDeDados.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCompromissos.Testes.ControladorTestes
{
    [TestClass]
    public class ControladorCompromissoTestes
    {
        [TestMethod]
        public void DeveInserirUmCompromisso()
        {
            //arrange
            Compromisso compromisso = new Compromisso("assunto", "local", Convert.ToDateTime("10/10/2000"), Convert.ToDateTime("13:00"), Convert.ToDateTime("14:00"), "Link", 1);
            

            ControladorCompromisso controlador = new ControladorCompromisso();

            //action
            controlador.InserirNovo(compromisso);

            //assert
            Assert.IsTrue(compromisso.id > 0);

            int id = compromisso.id;

            Compromisso compromissoEncontrado = controlador.SelecionarRegistroPorId(id);
            Assert.AreEqual("local", compromissoEncontrado.local);
            Assert.AreEqual(Convert.ToDateTime("10/10/2000"), compromissoEncontrado.data);
        }

        [TestMethod]
        public void DeveAtualizarUmCompromisso()
        {
            //arrange
            Compromisso compromisso1 = new Compromisso("assunto1", "local1", Convert.ToDateTime("11/11/2000"), Convert.ToDateTime("11:00"), Convert.ToDateTime("12:00"), "Link", 1);

            ControladorCompromisso controlador = new ControladorCompromisso();
            controlador.InserirNovo(compromisso1);
            int id = compromisso1.id;

            //action
            Compromisso compromisso2 = new Compromisso("assunto2", "local2", Convert.ToDateTime("12/12/2000"), Convert.ToDateTime("12:00"), Convert.ToDateTime("13:00"), "Link", 2);
            controlador.EditarRegistro(id, compromisso2);

            //assert
            Compromisso compromissoAtualizado = controlador.SelecionarRegistroPorId(id);
            Assert.AreEqual("assunto2", compromissoAtualizado.assunto);
            Assert.AreEqual(Convert.ToDateTime("12:00"), compromissoAtualizado.horaInicio);
        }

        [TestMethod]
        public void DeveExcluirUmCompromisso()
        {
            //arrange
            Compromisso compromisso = new Compromisso("assunto", "local", Convert.ToDateTime("12/12/2000"), Convert.ToDateTime("12:00"), Convert.ToDateTime("13:00"), "Link", 1);
            ControladorCompromisso controlador = new ControladorCompromisso();

           
            controlador.InserirNovo(compromisso);
            int id = compromisso.id;

            //action            
            controlador.ExcluirRegistro(id);

            //assert
            Compromisso compromissoEncontrado = controlador.SelecionarRegistroPorId(id);
            Assert.IsNull(compromissoEncontrado);
        }
    }
}
