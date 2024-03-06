using Microsoft.EntityFrameworkCore;
using VokzFeedback.Models;

namespace VokzFeedback.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
