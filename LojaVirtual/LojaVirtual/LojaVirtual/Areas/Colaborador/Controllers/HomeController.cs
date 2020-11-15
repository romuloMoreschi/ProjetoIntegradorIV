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
        private IUsuarioRepository _repositoryUsuario;
        private LoginUsuario _loginUsuario;
        public HomeController(IUsuarioRepository repositoryColaborador, LoginUsuario loginUsuario)
        {
            _repositoryUsuario = repositoryColaborador;
            _loginUsuario = loginUsuario;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Models.Usuario usuario)
        {
            Models.Usuario usuariodb = _repositoryUsuario.Login(usuario.Email, usuario.Senha);

            if (usuariodb != null)
            {
                //Fazer consulta no banco de dados e armazenar na sesscao
                //o que pode ser guradado na secao (EMAIL,ID,SENHA,NOME ETC)
                _loginUsuario.Login(usuariodb);
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
        [UsuarioAutorizacaoAtribute]
        public IActionResult Logout() 
        {
            _loginUsuario.Logout();
            return RedirectToAction("Login", "Home");
        }

        [UsuarioAutorizacaoAtribute]
        public IActionResult Painel()
        {
            return View();
        }
    }
}


