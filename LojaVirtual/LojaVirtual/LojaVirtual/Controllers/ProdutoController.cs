using System.Linq;
using System;
using LojaVirtual.ViewModel;
using LojaVirtual.Database;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Repository.Contract;
using X.PagedList;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index(int? pagina)
        {
            var produtos = _produtoRepository.ObterTodosProdutos(pagina).Select(p => new ProdutoViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Valor = p.Valor,
                ImagemByte = p.Imagem
            }).AsEnumerable(); ;


            foreach (var produto in produtos)
            {
                if(produto.ImagemByte != null)
                {
                    var base64 = Convert.ToBase64String(produto.ImagemByte);
                    produto.ImagemString = String.Format("data:image/jpg;base64,{0}", base64);
                }                
            }

            return View(produtos);
        }
    }
}
