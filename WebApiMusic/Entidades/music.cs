namespace WebApiMusic.Entidades
{
    public class Music
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string Discografia { get; set; }
        public int ArtistaId { get; set; }
    }
}
