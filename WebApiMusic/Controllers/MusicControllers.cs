﻿using Microsoft.AspNetCore.Mvc;
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
            return await dbContext.musica.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Music musica)
        {
            dbContext.Add(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Music musica, int id)
        {
            if(musica.Id != id)
            {
                return BadRequest("El id tanto de 'musica' como el de la 'URL' no coinciden, favor de verificarlos ");
            }
            dbContext.Update(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Music musica)
        {
            dbContext.Remove(musica);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
