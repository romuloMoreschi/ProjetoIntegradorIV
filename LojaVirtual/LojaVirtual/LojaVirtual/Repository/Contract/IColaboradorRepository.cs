using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repository.Contract
{
   public interface IColaboradorRepository
    {
        Colaborador Login(string Email, string senha);

        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void Excluir(int Id);
        Colaborador ObterColaborador(int Id);
        IEnumerable<Colaborador> ObterTodosColaborardores();

    }
}
