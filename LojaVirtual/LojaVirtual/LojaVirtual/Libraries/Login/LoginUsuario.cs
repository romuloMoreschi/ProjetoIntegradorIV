using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginUsuario
    {
        private string Key = "Login.Usuario";
        private Session.Session _session;
        public LoginUsuario(Session.Session session)
        {
            _session = session;
        }

        public void Login(Usuario usuario)
        {
            //Armazenar na sessao 
            //serializar com o nuget - NewTom Json
            string usuarioJsonstring = JsonConvert.SerializeObject(usuario);
            _session.Cadastrar(Key, usuarioJsonstring);
        }
        public Usuario GetUsuario()
        {
            //Deserializar
            if (_session.Existe(Key))
            {
                string usuarioJsonstring = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Usuario>(usuarioJsonstring);
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
