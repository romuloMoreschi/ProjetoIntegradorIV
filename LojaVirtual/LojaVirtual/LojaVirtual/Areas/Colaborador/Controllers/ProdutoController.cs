using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using LojaVirtual.Repository.Contract;
using LojaVirtual.ViewModel;
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
            var produtos = _produtoRepository.ObterTodosProdutos(pagina).Select(p => new ProdutoViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Valor = p.Valor,
                ImagemByte = p.Imagem
            }); ;


            foreach (var produto in produtos)
            {
                if (produto.ImagemByte != null)
                {
                    var base64 = Convert.ToBase64String(produto.ImagemByte);
                    produto.ImagemString = String.Format("data:image/jpg;base64,{0}", base64);
                }
            }

            return View(produtos);
        }

        [HttpGet]
        public IActionResult Atualizar(int Id)
        {
            var produto = _produtoRepository.ObterProduto(Id);
            ViewBag.Produto = _produtoRepository.ObterTodosProdutos().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(produto);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] ProdutoViewModel produtoVm, int Id)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Atualizar(produtoVm);

                TempData["MSG_S"] = "Registro salvo com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ProdutoViewModel produtoVm)
        {
            if (ModelState.IsValid)
            {
                _produtoRepository.Cadastrar(produtoVm);

                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categoria = _produtoRepository.ObterTodosProdutos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        public IActionResult Detalhes(int id)
        {
            var produto = _produtoRepository.ObterProduto(id);

            if (produto.ImagemByte != null)
            {
                var base64 = Convert.ToBase64String(produto.ImagemByte);
                produto.ImagemString = String.Format("data:image/jpg;base64,{0}", base64);
            }

            return View(produto);
        }

        public IActionResult Excluir(int id)
        {
            _produtoRepository.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
