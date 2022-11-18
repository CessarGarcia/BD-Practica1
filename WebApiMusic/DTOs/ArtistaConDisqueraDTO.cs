namespace WebApiMusic.DTOs
{
    public class ArtistaConDisqueraDTO : GetArtistaDTO
    {
        public List<DisqueraDTO> Disquera { get; set; }
    }
}
