using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;

namespace WebApiMusic
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<music> Musica { get; set; }
    }
}
