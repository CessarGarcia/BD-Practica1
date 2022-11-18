using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusic.DTOs;
using WebApiMusic.Entidades;

namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("disqueras")]
    public class DisqueraController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DisqueraController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoDisquera")]
        public async Task<ActionResult<List<Disquera>>> GetAll()
        {
            return await dbContext.disquera.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerDisquera")]
        public async Task<ActionResult<DisqueraConArtistaDTO>> GetById(int id)
        {
            var disquera = await dbContext.disquera
                .Include(disqueraDB => disqueraDB.ArtistaDisquera)
                .ThenInclude(artistaDisqueraDB => artistaDisqueraDB.Artista)
                .Include(disqueraDB => disqueraDB.Music)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (disquera == null)
            {
                return NotFound();
            }

            disquera.ArtistaDisquera = disquera.ArtistaDisquera.OrderBy(x => x.Orden).ToList();

            return mapper.Map<DisqueraConArtistaDTO>(disquera);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DisqueraCreacionDTO disqueraCreacionDTO)
        {

            if (disqueraCreacionDTO.ArtistasIds == null)
            {
                return BadRequest("No se puede crear una disquera sin artistas.");
            }

            var artistaIds = await dbContext.artista
                .Where(artistaBD => disqueraCreacionDTO.ArtistasIds.Contains(artistaBD.Id)).Select(x => x.Id).ToListAsync();

            if (disqueraCreacionDTO.ArtistasIds.Count != artistaIds.Count)
            {
                return BadRequest("No existe el artista enviado");
            }

            var disquera = mapper.Map<Disquera>(disqueraCreacionDTO);

            OrdenarPorDisquera(disquera);

            dbContext.Add(disquera);
            await dbContext.SaveChangesAsync();

            var disqueraDTO = mapper.Map<DisqueraDTO>(disquera);

            return CreatedAtRoute("obtenerDisquera", new { id = disquera.Id }, disqueraDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, DisqueraCreacionDTO disqueraCreacionDTO)
        {
            var disqueraDB = await dbContext.disquera
                .Include(x => x.ArtistaDisquera)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (disqueraDB == null)
            {
                return NotFound();
            }

            disqueraDB = mapper.Map(disqueraCreacionDTO, disqueraDB);

            OrdenarPorDisquera(disqueraDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.disquera.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }
            dbContext.Remove(new Disquera { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        private void OrdenarPorDisquera(Disquera disquera)
        {
            if (disquera.ArtistaDisquera != null)
            {
                for (int i = 0; i < disquera.ArtistaDisquera.Count; i++)
                {
                    disquera.ArtistaDisquera[i].Orden = i;
                }
            }
        }
    }
}
