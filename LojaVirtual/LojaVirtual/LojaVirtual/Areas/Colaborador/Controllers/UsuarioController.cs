using LojaVirtual.Libraries.Lang;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.Libraries.Texto;
using X.PagedList;
using LojaVirtual.Libraries.Email;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private GerenciarEmail _gerenciarEmail;
        public UsuarioController(IUsuarioRepository usuario, GerenciarEmail gerenciarEmail)
        {
            _usuarioRepository = usuario;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Usuario> usuario = _usuarioRepository.ObterTodosUsuarios(pagina);
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View()
;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.TipoUsuario = "colaborador";
                _usuarioRepository.Cadastrar(usuario);
                TempData["MSG_S"] = Mensagem.MSG_S001;
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
