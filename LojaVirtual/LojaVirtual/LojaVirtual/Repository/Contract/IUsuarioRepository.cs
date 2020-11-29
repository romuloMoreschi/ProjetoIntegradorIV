using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repository.Contract
{
    public interface IUsuarioRepository
    {
        Usuario Login(string Email, string senha);
        //void CadastrarColaborador()
        void Cadastrar(Usuario cliente);
        void Atualizar(Usuario cliente);
        void Excluir(int Id);
        bool ConsultaTipo(string email);
        Usuario ObterUsuario(int Id);
        IEnumerable<Usuario> ObterTodosUsuarios();
        IPagedList<Usuario> ObterTodosUsuarios(int? pagina);
    }
}
