namespace WebApiMusic.Entidades
{
    public class ArtistaDisquera
    {
        public int ArtistaId { get; set; }
        public int DisqueraId { get; set; }
        public int Orden { get; set; }
        public Artista Artista { get; set; }
        public Disquera Disquera { get; set; }
    }
}
