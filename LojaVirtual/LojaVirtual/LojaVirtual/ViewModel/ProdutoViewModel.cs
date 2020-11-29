using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ViewModel
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        [Required(ErrorMessage ="Arquivo é obrigatório")]
        public IFormFile Imagem { get; set; }
        public byte[] ImagemByte { get; set; }
        public string ImagemString { get; set; }
    }
}