//Microsoft
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//WebApiMusic
using WebApiMusic.Entidades;
using WebApiMusic.DTOs;


namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("artistas")]
    public class ArtistaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ArtistaController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetArtistaDTO>>> Get()
        {
            var artista = await dbContext.artista.ToListAsync();
            return mapper.Map<List<GetArtistaDTO>>(artista);
        }


        [HttpGet("{id:int}", Name = "obtenerartista")] //Se puede usar ? para que no sea obligatorio el parametro /{param=Gustavo}  getArtista/{id:int}/
        public async Task<ActionResult<ArtistaConDisqueraDTO>> Get(int id)
        {
            var artista = await dbContext.artista
                .Include(artistaDB => artistaDB.ArtistaDisquera)
                .ThenInclude(artistaDisqueraDB => artistaDisqueraDB.Disquera)
                .FirstOrDefaultAsync(artistaBD => artistaBD.Id == id);

            if (artista == null)
            {
                return NotFound();
            }

            return mapper.Map<ArtistaConDisqueraDTO>(artista);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetArtistaDTO>>> Get([FromRoute] string nombre)
        {
            var artistas = await dbContext.artista.Where(artistaBD => artistaBD.NombreArtistico.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetArtistaDTO>>(artistas);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ArtistaDTO artistaDto)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeartistaMismoNombre = await dbContext.artista.AnyAsync(x => x.NombreArtistico == artistaDto.NombreArtistico);

            if (existeartistaMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {artistaDto.NombreArtistico}");
            }

            var artista = mapper.Map<Artista>(artistaDto);

            dbContext.Add(artista);
            await dbContext.SaveChangesAsync();

            var artistaDTO = mapper.Map<GetArtistaDTO>(artista);

            return CreatedAtRoute("obtenerArtista", new { id = artista.Id }, artistaDTO);
        }

        [HttpPut("{id:int}")] // api/artista/1
        public async Task<ActionResult> Put(ArtistaDTO artistaCreacionDTO, int id)
        {
            var exist = await dbContext.artista.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            var artista = mapper.Map<Artista>(artistaCreacionDTO);
            artista.Id = id;

            dbContext.Update(artista);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.artista.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }

            dbContext.Remove(new Artista()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}