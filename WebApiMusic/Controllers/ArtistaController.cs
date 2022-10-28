//Microsoft
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//WebApiMusic
using WebApiMusic.Entidades;



namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("artista")]
    public class ArtistaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ArtistaController> log;
        

        public ArtistaController(ApplicationDbContext context, ILogger<ArtistaController> logger)
        {
            this.dbContext = context;
            this.log = logger;
        }
        /*Get Post Put Delete*/
        
        [HttpGet]
        [HttpGet("/listadoArtistas")]
        public async Task<ActionResult<List<Artista>>> GetAll()
        {
            log.LogInformation("Obteniendo listado de artistas");
            return await dbContext.artista.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Artista>> GetById(int id)
        {
            log.LogInformation("EL ID ES: " + id);
            return await dbContext.artista.FirstOrDefaultAsync(x => x.Id == id);
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