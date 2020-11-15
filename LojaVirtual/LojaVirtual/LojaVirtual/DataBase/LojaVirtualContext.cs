using LojaVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Database
{
    public class LojaVirtualContext : DbContext
    {
        /*
         * EF Core - ORM
         * ORM -> Bibliteca mapear Objetos para Banco de Dados Relacionais
         */
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
        {

        }

        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Produto> Produto { get; set; }
    }
}
