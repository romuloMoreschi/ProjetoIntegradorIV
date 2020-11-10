using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        public ColaboradorController(IColaboradorRepository colaborador)
        {
            _colaboradorRepository = colaborador;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Colaborador> colaboradores = _colaboradorRepository.ObterTodosColaborardores(pagina);
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View()
;
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                colaborador.Tipo = "C";
                _colaboradorRepository.Cadastrar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        [HttpGet]
        public IActionResult Atualizar(int Id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(Id);

            return View(colaborador);

        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int Id)
        {
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S003;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Excluir(int Id)
        {
            _colaboradorRepository.Excluir(Id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}
