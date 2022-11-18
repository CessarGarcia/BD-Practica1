using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;

using WebApiMusic.DTOs;


namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("disqueras/{disqueraId:int}/music")]
    public class MusicController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public MusicController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MusicDTO>>> Get(int disqueraId)
        {
            var existeDisquera = await dbContext.disquera.AnyAsync(disqueraDB => disqueraDB.Id == disqueraId);
            if (!existeDisquera)
            {
                return NotFound();
            }

            var disquera = await dbContext.musica.Where(disqueraDB => disqueraDB.DisqueraId == disqueraId).ToListAsync();

            return mapper.Map<List<MusicDTO>>(disquera);
        }

        [HttpGet("{id:int}", Name = "obtenerMusic")]
        public async Task<ActionResult<MusicDTO>> GetById(int id)
        {
            var disquera = await dbContext.musica.FirstOrDefaultAsync(disqueraDB => disqueraDB.Id == id);

            if (disquera == null)
            {
                return NotFound();
            }

            return mapper.Map<MusicDTO>(disquera);
        }
        [HttpPost]
        public async Task<ActionResult> Post(int disqueraId, MusicCreacionDTO musicaCreacionDTO)
        {
            var existeDisquera = await dbContext.disquera.AnyAsync(disqueraDB => disqueraDB.Id == disqueraId);
            if (!existeDisquera)
            {
                return NotFound();
            }

            var musica = mapper.Map<Music>(musicaCreacionDTO);
            musica.DisqueraId = disqueraId;
            dbContext.Add(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    

    [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int disqueraId, int id, MusicCreacionDTO musicaCreacionDTO)
        {
            var existeDisquera = await dbContext.disquera.AnyAsync(disqueraDB => disqueraDB.Id == disqueraId);
            if (!existeDisquera)
            {
                return NotFound();
            }

            var existeMusica = await dbContext.musica.AnyAsync(musicaDB => musicaDB.Id == id);
            if (!existeMusica)
            {
                return NotFound();
            }

            var musica = mapper.Map<Music>(musicaCreacionDTO);
            musica.Id = id;
            musica.DisqueraId = disqueraId;

            dbContext.Update(musica);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
