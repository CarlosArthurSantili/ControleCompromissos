using BancoDeDados.ConsoleApp;
using BancoDeDados.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleTarefas.Testes.DominioTestes
{
    [TestClass]
    public class ContatoTestes
    {
        [TestMethod]
        public void EmailValido()
        {
            string email = "a@gmail.com";
            Contato teste = new Contato("",email,0,"","");

            Assert.AreEqual(true, teste.ValidarEmail());
            Assert.AreEqual(false, teste.Validar());
        }

        [TestMethod]
        public void EmailInvalido()
        {
            string email = "aaa";
            Contato teste = new Contato("", email, 0, "", "");

            Assert.AreEqual(false, teste.ValidarEmail());
            Assert.AreEqual(false, teste.Validar());
        }

        [TestMethod]
        public void TelefoneValido()
        {
            int telefone = 999111222;
            Contato teste = new Contato("", "", telefone, "", "");

            Assert.AreEqual(true, teste.ValidarTelefone());
            Assert.AreEqual(false, teste.Validar());
        }

        [TestMethod]
        public void TelefoneInvalido()
        {
            int telefone = 1234;
            Contato teste = new Contato("", "", telefone, "", "");

            Assert.AreEqual(false, teste.ValidarTelefone());
            Assert.AreEqual(false, teste.Validar());
        }
    }
}
