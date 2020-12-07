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
        readonly ICategoriaRepository _categoriaRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index(int? pagina)
        {
            var produtos = _produtoRepository.ObterTodosProdutos(pagina).AsEnumerable(); ;

            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias();

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
