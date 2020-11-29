using System.Linq;
using System;
using LojaVirtual.ViewModel;
using LojaVirtual.Database;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly LojaVirtualContext _context;

        public ProdutoController(LojaVirtualContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
             var produtos = _context.Produto.Select(p => new ProdutoViewModel { 
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Valor = p.Valor,
                ImagemByte = p.Imagem
            });;


            foreach(var produto in produtos)
            {
                if(produto.ImagemByte != null)
                {
                    var base64 = Convert.ToBase64String(produto.ImagemByte);
                    produto.ImagemString = String.Format("data:image/jpg;base64,{0}", base64);
                }                
            }

            return View(produtos);
        }
    }
}
