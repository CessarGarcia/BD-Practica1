using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;

namespace WebApiMusic
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Music> musica { get; set; }
        public DbSet<Artista> artista { get; set; }
    }
}
