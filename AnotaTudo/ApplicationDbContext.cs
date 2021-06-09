using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnotaTudo
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Ficha> Fichas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
