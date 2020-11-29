using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Database;
using LojaVirtual.Models;
using System;
using LojaVirtual.ViewModel;
using LojaVirtual.Repository.Contract;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly LojaVirtualContext _context;
        readonly IProdutoRepository _produtoRepository;

        public ProdutoController(LojaVirtualContext context, IProdutoRepository produtoRepository)
        {
            _context = context;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index(int? pagina)
        {
             var produtos = _produtoRepository.ObterTodosProdutos(pagina).Select(p => new ProdutoViewModel { 
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
