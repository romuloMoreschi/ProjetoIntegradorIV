using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using LojaVirtual.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        const int RegistroPorPagina = 10;
        readonly LojaVirtualContext _banco;
        public ProdutoRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(ProdutoViewModel produtoVm)
        {
            var produto = MapeiaVmToProduto(produtoVm);
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(ProdutoViewModel produtoVm)
        {
            var produto = MapeiaVmToProduto(produtoVm);
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            _banco.Remove(_banco.Produto.Find(Id));
            _banco.SaveChanges();
        }

        public Produto MapeiaVmToProduto(ProdutoViewModel produtoVm)
        {
            using (var memoryStream = new MemoryStream())
            {
                produtoVm.Imagem.CopyToAsync(memoryStream);

                // Upload the file if less than 20 MB
                if (memoryStream.Length < 20097152)
                {
                    Produto produto = new Produto
                    {
                        Id = produtoVm.Id,
                        Nome = produtoVm.Nome,
                        Descricao = produtoVm.Descricao,
                        Valor = produtoVm.Valor,
                        Imagem = memoryStream.ToArray()
                    };

                    return produto;
                }
                else
                {
                    return null;
                }
            }
        }

        public ProdutoViewModel ObterProduto(int Id)
        {
            return MapeiaProdutoToVm(_banco.Produto.Find(Id));
        }

        public ProdutoViewModel MapeiaProdutoToVm (Produto produto)
        {
            ProdutoViewModel _produto = new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Valor = produto.Valor,
                ImagemByte = produto.Imagem
            };

            return _produto;
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
