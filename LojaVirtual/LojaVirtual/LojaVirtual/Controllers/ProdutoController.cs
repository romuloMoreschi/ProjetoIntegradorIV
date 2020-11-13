using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Database;
using LojaVirtual.Models;

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
            return View(_context.Produto);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Cadastrar(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
