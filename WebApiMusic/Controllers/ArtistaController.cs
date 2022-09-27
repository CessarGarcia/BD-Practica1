using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiArtistas.Entidades;
using WebApiMusic;

namespace WebApiArtistas.Controllers
{
    [ApiController]
    [Route("api/artista")]
    public class ArtistaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ArtistaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
                 /*Get Post Put Delete*/
        [HttpGet]
        public async Task<ActionResult<List<Artista>>> Get()
        {
            return await dbContext.artista.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Artista artista)
        {
            dbContext.Add(artista);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Artista artista, int id)
        {
            if (artista.Id != id)
            {
                return BadRequest("El id tanto de 'musica' como el de la 'URL' no coinciden, favor de verificarlos ");
            }
            dbContext.Update(artista);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Artista artista)
        {
            dbContext.Remove(artista);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
