using LojaVirtual.Models;
using LojaVirtual.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repository.Contract
{
    public interface IProdutoRepository
    {
        void Cadastrar(ProdutoViewModel produto);
        void Atualizar(ProdutoViewModel produto);
        void Excluir(int Id);
        Produto MapeiaVmToProduto(ProdutoViewModel produto);
        ProdutoViewModel MapeiaProdutoToVm(Produto produto);
        ProdutoViewModel ObterProduto(int Id);
        IEnumerable<Produto> ObterTodosProdutos();
        IPagedList<Produto> ObterTodosProdutos(int? pagina);
    }
}
