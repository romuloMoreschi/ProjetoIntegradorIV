using System.Linq;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //[ColaboradorAutorizacaoAtribute]
    public class ProdutoController : Controller
    {
        readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index(int? pagina)
        {
            var produtos = _produtoRepository.ObterTodosProdutos(pagina);
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Editar(int Id)
        {
            var produto = _produtoRepository.ObterProduto(Id);
            ViewBag.Produto = _produtoRepository.ObterTodosProdutos().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(produto);
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Produto produto, int Id)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Atualizar(produto);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categoria = _produtoRepository.ObterTodosProdutos().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult<Produto> Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Cadastrar(produto);

                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categoria = _produtoRepository.ObterTodosProdutos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
    }
}
