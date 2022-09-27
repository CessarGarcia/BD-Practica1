using WebApiMusic.Entidades;

namespace WebApiArtistas.Entidades
{
    public class Artista
    {
        public int Id { get; set; }
        public string NombreArtistico { get; set; }
        public string NombreReal {get; set; }
        public string Entidad {get; set; }
        public int Edad { get; set;  }

        public List<Music> Musics { get; set; }
    }
}
