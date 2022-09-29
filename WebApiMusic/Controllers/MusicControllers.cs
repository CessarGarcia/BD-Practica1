using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;

namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("api/music")]
    public class MusicControllers : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public MusicControllers(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public object Context { get; }

        [HttpGet]
        public async Task<ActionResult<List<Music>>> Get()
        {
            return await dbContext.musica.Include(x => x.Artista).ToListAsync();
        }

       [HttpGet("{id:int}")]
        public async Task<ActionResult<Music>> GetById(int id)
        {
            return await dbContext.musica.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Music musica)
        {
            var existeMusic = await dbContext.musica.AnyAsync(x => x.Id == musica.ArtistaId);
            if (!existeMusic)
            {
                return BadRequest($"No existe la musica con el id: {musica.ArtistaId}");
            }
            dbContext.Add(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Music musica, int id)
        {
            var exist = await dbContext.musica.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No existe");
            }
            if (musica.Id != id)
            {
                return BadRequest("El id tanto de 'musica' como el de la 'URL' no coinciden, favor de verificarlos ");
            }
            dbContext.Update(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.musica.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No existe");
            }
            dbContext.Remove(new Music { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
