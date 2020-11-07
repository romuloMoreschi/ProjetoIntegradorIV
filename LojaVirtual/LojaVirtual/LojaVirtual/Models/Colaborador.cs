using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }

        /*Tipos de colaboradores
         * C== Comum
         * G==Gerente
         */
        public string Tipo { get; set; }


    }
}
