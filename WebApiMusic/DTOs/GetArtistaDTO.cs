namespace WebApiMusic.DTOs
{
    public class GetArtistaDTO
    {
        public int Id { get; set; }
        public string NombreArtistico { get; set; }

        public string NombreReal { get; set; }
        public string Entidad { get; set; }

        public int Edad { get; set; }
    }
}
