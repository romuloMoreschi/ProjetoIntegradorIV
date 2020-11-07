using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filter;
using LojaVirtual.Migrations;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //TODO - Habilitar Verificação de Login
    [ColaboradorAutorizacaoAtribute]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index(int? pagina)
        {
            /*Fazendo a paginacao
             * faz a consulta com IpagedList, buscando somente 10 registros
             * 
             */
            var categorias = _categoriaRepository.ObterTodasCategorias(pagina);
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categoria = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();

        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Cadastrar(categoria);

                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categoria = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();

        }
        [HttpGet]
        public IActionResult Atualizar(int Id)
        {
            var categoria = _categoriaRepository.ObterCategoria(Id); // Recece pelo ID para passar para a tela atualizar
            ViewBag.Categoria = _categoriaRepository.ObterTodasCategorias().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(categoria);


        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Categoria categoria, int Id)
        {

            if (ModelState.IsValid)
            {
                _categoriaRepository.Atualizar(categoria);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categoria = _categoriaRepository.ObterTodasCategorias().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            _categoriaRepository.Excluir(Id);
            TempData["MSG_S"] = "Registro Excluido com Sucesso";

            return RedirectToAction(nameof(Index));
        }


    }
}
