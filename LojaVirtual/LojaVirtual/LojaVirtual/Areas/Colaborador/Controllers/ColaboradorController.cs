using LojaVirtual.Libraries.Lang;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Libraries.Texto;
using X.PagedList;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models.Constantes;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private GerenciarEmail _gerenciarEmail;
        public ColaboradorController(IUsuarioRepository usuario, GerenciarEmail gerenciarEmail)
        {
            _usuarioRepository = usuario;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
            var usuario = _usuarioRepository.ObterTodosColaboradores(pagina);

            return View(usuario);
        }

        [HttpGet]
        public IActionResult CadastroColaborador()
        {
            return View()
;
        }       

        [HttpPost]
        public IActionResult CadastroColaborador([FromForm] Models.Usuario usuario)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                //TODO - Gerar Senha Aleatorio, Enviar o E-mail do Colaborador
                usuario.TipoUsuario = UsuarioTipoConstante.Colaborador;
                usuario.Senha = KeyGenerator.GetUniqueKey(8);
                _usuarioRepository.Cadastrar(usuario);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                //Tem que fazer um modo async para verificar se o email foi enviado ou não
                _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(usuario);
                TempData["MSG_S"] = Mensagem.MSG_S004;
                return RedirectToAction(nameof(Index));

            }
            return View();
        }


        public IActionResult GerarSenha(int Id)
        {
            Models.Usuario usuario = _usuarioRepository.ObterUsuario(Id);
            usuario.Senha = KeyGenerator.GetUniqueKey(8);
            _usuarioRepository.Atualizar(usuario);
            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(usuario);
            TempData["MSG_S"] = Mensagem.MSG_S004;
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Atualizar(int Id)
        {
            Models.Usuario colaborador = _usuarioRepository.ObterUsuario(Id);

            return View(colaborador);

        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Usuario usuario, int Id)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Atualizar(usuario);
                TempData["MSG_S"] = Mensagem.MSG_S003;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Excluir(int Id)
        {
            _usuarioRepository.Excluir(Id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}
