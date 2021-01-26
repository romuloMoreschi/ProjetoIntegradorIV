using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Texto;
using LojaVirtual.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ClienteController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private GerenciarEmail _gerenciarEmail;
        public ClienteController(IUsuarioRepository usuario, GerenciarEmail gerenciarEmail)
        {
            _usuarioRepository = usuario;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Usuario> usuario = _usuarioRepository.ObterTodosClientes(pagina);
            return View(usuario);
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
        public IActionResult Excluir(int Id)
        {
            _usuarioRepository.Excluir(Id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}
