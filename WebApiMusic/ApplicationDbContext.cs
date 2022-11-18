using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;

namespace WebApiMusic
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistaDisquera>()
                .HasKey(al => new { al.ArtistaId, al.DisqueraId });
        }

        public DbSet<Music> musica { get; set; }
        public DbSet<Artista> artista { get; set; }
        public DbSet<Disquera> disquera { get; set; }
        public DbSet<ArtistaDisquera> ArtistaDisquera { get; set; }
    }
}
