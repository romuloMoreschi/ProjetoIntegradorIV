using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Session
{
    public class Session
    {
        //Injecao depependencia para manipulacao da secao dentro da classe
        private IHttpContextAccessor _context;
        public Session(IHttpContextAccessor context)
        {
            _context = context;
        }

        //CRUD - Cadastra, consulta,atualizar, remover - RemoverTodos/SeExiste



        public void Cadastrar(string Key, string Valor)
        {
            _context.HttpContext.Session.SetString(Key, Valor);

        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                _context.HttpContext.Session.Remove(Key);

            }
            _context.HttpContext.Session.SetString(Key, Valor);
        }

        public void Remover(string Key)
        {
            _context.HttpContext.Session.Remove(Key);
        }

        public string Consultar(string Key)
        {
            return _context.HttpContext.Session.GetString(Key);
        }

        public bool Existe(string Key)
        {
            if (_context.HttpContext.Session.GetString(Key)==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoverTodos()
        {
            _context.HttpContext.Session.Clear();
        }
    }
}
