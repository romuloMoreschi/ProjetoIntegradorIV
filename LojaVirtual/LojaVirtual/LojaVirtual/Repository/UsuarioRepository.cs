﻿using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        const int RegistroPorPagina = 10;

        private LojaVirtualContext _banco;
        public UsuarioRepository(LojaVirtualContext banco)
        {
            _banco = banco;

        }
        public void Atualizar(Usuario cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public void Cadastrar(Usuario cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Usuario usuario = ObterUsuario(Id);
            _banco.Remove(usuario);
            _banco.SaveChanges();
        }

        public Usuario Login(string Email, string senha)
        {
            Usuario usuario = _banco.Usuario.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();
            return usuario;
        }

        public Usuario ObterUsuario(int Id)
        {
            return _banco.Usuario.Find(Id);
        }

        public IPagedList<Usuario> ObterTodosColaboradores(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Usuario.Where(p => p.TipoUsuario == "COLABORADOR").ToPagedList<Usuario>(numeroPagina, RegistroPorPagina); ;
        }
        public IPagedList<Usuario> ObterTodosClientes(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Usuario.Where(p => p.TipoUsuario == "CLIENTE").ToPagedList<Usuario>(numeroPagina, RegistroPorPagina); ;
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            return _banco.Usuario;
        }
        
        public bool ConsultaTipo (string email)
        {
            var user = _banco.Usuario.Where(p => p.Email == email).Select(p => p.TipoUsuario).FirstOrDefault();
            return user == "CLIENTE";
        }
    }
}
