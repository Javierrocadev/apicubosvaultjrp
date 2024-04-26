using apicubosvaultjrp.Models;
using Microsoft.EntityFrameworkCore;

namespace apicubosvaultjrp.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options)
        {
        }

        public DbSet<Cubo> Cubos { get; set; }

        public DbSet<UsuarioCubo> Usuarios { get; set; }
    }
}
