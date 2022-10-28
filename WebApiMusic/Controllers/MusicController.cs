using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMusic.Entidades;
using WebApiMusic.Filtros;
using WebApiMusic.Migrations;
using WebApiMusic.Services;

namespace WebApiMusic.Controllers
{
    [ApiController]
    [Route("api/music")] // api/music2 
    public class MusicController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<MusicController> logger;
        private readonly IWebHostEnvironment env;
        private readonly string nuevosRegistros = "nuevosRegistros.txt";
        private readonly string registrosConsultados = "registrosConsultados.txt";

        public MusicController(ApplicationDbContext dbContext, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<MusicController> logger,
           IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                MusicControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                MusicControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                MusicControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado")]
        //[ResponseCache(Duration = 15)]
        //[Authorize]
        //[ServiceFilter(typeof(FiltroDeAccion))]
        public async Task<ActionResult<List<Music>>> GetMusic()
        {
            throw new NotImplementedException();
            /*
            logger.LogInformation("Se obtiene el listado de artistas");
            logger.LogWarning("Mensaje de prueba warning");
            */
            service.EjecutarJob();
            return await dbContext.musica.Include(x => x.Artista).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Music>> PrimeraMusic ()
        {
            return await dbContext.musica.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Music> PrimeraMusicA()
        {
            return new Music { Artista = "Hola" };
        }

        [HttpGet("{param?}")] //Se usa ? para no hacer obligatorio el parametro
        public async Task<ActionResult<Music>> Get(int id, string param)
        {
            var music = await dbContext.musica.FirstOrDefaultAsync(x => x.Id == id);
            if(music == null)
            {
                return NotFound();
            }
            return music;
        }

        [HttpGet("obtenerMusic/{SongName}")]
        public async Task<ActionResult<Music>> Get([FromRoute] string SongName)
        {
            var music = await dbContext.musica.FirstOrDefaultAsync(x => x.SongName.Contains(SongName));
            if(music == null)
            {
                logger.LogError("No se encuentra el nombre de la cancion.");
                return NotFound();
            }
            var ruta = $@"{env.ContentRootPath}\wwwroot\{registrosConsultados}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(music.Id + " " + music.SongName); }
            return music;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Music music)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext
            var existeSongMismoNombre = await dbContext.musica.AnyAsync(x => x.SongName == music.SongName);
            if (existeSongMismoNombre)
            {
                return BadRequest("Ya existe un autor con el nombre");
            }

            dbContext.Add(music);
            await dbContext.SaveChangesAsync();
            //   var ruta = $@"{env.ContentRootPath}\wwwroot\{nuevosRegistros}";
            //  using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(music.Id + " " + music.SongName); }
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Music music, int id)
        {
            var exist = await dbContext.musica.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            if (music.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url.");
            }

            dbContext.Update(music);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.musica.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }
            dbContext.Remove(new Music()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
       
    }
}
