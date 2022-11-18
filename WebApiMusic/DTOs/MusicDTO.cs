using System.ComponentModel.DataAnnotations;
using WebApiMusic.Validaciones;

namespace WebApiMusic.DTOs
{
    public class MusicDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener hasta 5 caracteres")]

        public string SongName { get; set; }

        [PrimeraLetraMayuscula]
        public string Artista { get; set; }
        public string Album { get; set; }
    }
}
