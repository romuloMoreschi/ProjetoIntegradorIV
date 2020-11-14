using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        const int RegistroPorPagina = 10;

        LojaVirtualContext _banco;
        public ProdutoRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(Produto produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Produto produto = ObterProduto(Id);
            _banco.Remove(produto);
            _banco.SaveChanges();
        }

        public Produto ObterProduto(int Id)
        {
            return _banco.Produto.Find(Id);
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Produto.ToPagedList<Produto>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            return _banco.Produto;
        }
    }
}
