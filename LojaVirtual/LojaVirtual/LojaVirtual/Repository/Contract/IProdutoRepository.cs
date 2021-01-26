using LojaVirtual.Models;
using LojaVirtual.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
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
        IPagedList<ProdutoViewModel> ObterTodosProdutos(int? pagina);
        IQueryable<SelectListItem> ObterCategorias();
        IPagedList<ProdutoViewModel> FiltraRegistro(int Id);
    }
}
