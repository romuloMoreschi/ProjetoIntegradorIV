using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repository.Contract
{
    public interface IProdutoRepository
    {
        void Cadastrar(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(int Id);
        Produto ObterProduto(int Id);
        IEnumerable<Produto> ObterTodosProdutos();
        IPagedList<Produto> ObterTodosProdutos(int? pagina);
    }
}
