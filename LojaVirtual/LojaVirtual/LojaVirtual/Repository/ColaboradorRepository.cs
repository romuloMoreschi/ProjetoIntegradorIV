using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repository
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private LojaVirtualContext _banco;
        private IConfiguration _conf;
        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Colaborador colaborador = ObterColaborador(Id);
            _banco.Remove(colaborador);
            _banco.SaveChanges();
        }

        public Colaborador Login(string Email, string senha)
        {
            Colaborador colaborador = _banco.Colaboradores.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaborador(int Id)
        {
            return _banco.Colaboradores.Find(Id);
        }

        public IPagedList<Colaborador> ObterTodosColaborardores(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _banco.Colaboradores.Where(a => a.Tipo != "G").ToPagedList<Colaborador>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }
    }
}
