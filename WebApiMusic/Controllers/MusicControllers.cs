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
        public async Task<ActionResult<List<music>>> Get()
        {
            return await dbContext.Musica.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(music musica)
        {
            dbContext.Add(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
