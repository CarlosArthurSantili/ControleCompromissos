using BancoDeDados.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados.ConsoleApp.Controladores
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public abstract bool InserirNovo(T registro);

        public abstract T SelecionarRegistroPorId(int id);

        public abstract bool EditarRegistro(int id, T registro);

        public abstract bool ExcluirRegistro(int id);
    }
}
