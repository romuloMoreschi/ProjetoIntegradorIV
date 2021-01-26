using System;
using System.Collections.Generic;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Filter;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {

        private IUsuarioRepository _repositoryUsuario;
        private INewsletterRepository _repositoryNewsletter;
        private LoginUsuario _loginUsuario;
        public HomeController(IUsuarioRepository repositoryUsuario, INewsletterRepository repositoryNewsletter, LoginUsuario loginUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryNewsletter = repositoryNewsletter;
            _loginUsuario = loginUsuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult(Url.Action("Index", "Produto"));
            // return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsletter.Cadastrar(newsletter);
                TempData["MSG_S"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    GerenciarEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }


            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamente mais tarde!";

                //TODO - Implementar Log
            }


            return View("Contato");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Usuario usuario)
        {
            Usuario clienteDb = _repositoryUsuario.Login(usuario.Email, usuario.Senha);

            if (clienteDb != null)
            {
                _loginUsuario.Login(clienteDb);

                bool _tipoDb = _repositoryUsuario.ConsultaTipo(clienteDb.Email);

                if (_tipoDb)
                {
                    return new RedirectResult(Url.Action(nameof(Index)));
                }
                else
                {
                    return new RedirectResult(Url.Action("Index", "Colaborador", new { Area = "Colaborador" }));
                }

            }
            else
            {
                ViewData["MSg_E"] = "Cliente não encontrado, verifique email e senha digitado";
                return View();

            }
        }

        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm] Usuario cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.TipoUsuario = "CLIENTE";
                _repositoryUsuario.Cadastrar(cliente);
                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                //TODO - Implementar redirecionamentos diferentes (Painel, Carrinho de Compras etc).
                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}