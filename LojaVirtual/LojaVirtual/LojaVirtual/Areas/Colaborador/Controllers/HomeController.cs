using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filter;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository _repositoryColaborador;
        private LoginColaborador _loginColaborador;
        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradordb = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradordb != null)
            {
                //Fazer consulta no banco de dados e armazenar na sesscao
                //o que pode ser guradado na secao (EMAIL,ID,SENHA,NOME ETC)
                _loginColaborador.Login(colaboradordb);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSg_E"] = "Usuario não encontrado, verifique email e senha digitado";
                return View();

            }
        }
       

        public IActionResult RecupararSenha()
        {
            return View();
        }
        public IActionResult CadastrarNovaSenha()
        {
            return View();
        }
        [ColaboradorAutorizacaoAtribute]
        public IActionResult Logout() 
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }

        [ColaboradorAutorizacaoAtribute]
        public IActionResult Painel()
        {
            return View();
        }
    }
}


