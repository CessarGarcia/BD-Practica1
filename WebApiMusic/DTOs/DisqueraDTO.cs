using System.ComponentModel.DataAnnotations;
using WebApiMusic.Entidades;
using WebApiMusic.Validaciones;

namespace WebApiMusic.DTOs
{
    public class DisqueraDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<Music> Music { get; set; }
    }
}
