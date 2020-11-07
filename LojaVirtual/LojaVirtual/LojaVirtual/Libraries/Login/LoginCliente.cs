using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Session.Session _session;
        public LoginCliente(Session.Session session)
        {
            _session = session;
        }

        public void Login(Cliente cliente)
        {
            //Armazenar na sessao 
            //serializar com o nuget - NewTom Json
            string clienteString = JsonConvert.SerializeObject(cliente);

            _session.Cadastrar(Key, clienteString);

        }
        public Cliente GetCLiente()
        {
            //Deserializar
            if (_session.Existe(Key))
            {
                string clienteString = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteString);
            }
            else 
            {
                return null;
            }


        }
        public void Logout()
        {
            _session.RemoverTodos();

        }

    }
}
