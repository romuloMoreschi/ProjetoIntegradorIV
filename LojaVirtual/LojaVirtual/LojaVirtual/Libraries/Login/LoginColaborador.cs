using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Session.Session _session;
        public LoginColaborador(Session.Session session)
        {
            _session = session;
        }

        public void Login(Colaborador colaborador)
        {
            //Armazenar na sessao 
            //serializar com o nuget - NewTom Json
            string colaboradorJsonstring = JsonConvert.SerializeObject(colaborador);

            _session.Cadastrar(Key, colaboradorJsonstring);

        }
        public Colaborador GetColaborador()
        {
            //Deserializar
            if (_session.Existe(Key))
            {
                string colaboradorJsonstring = _session.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJsonstring);
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
