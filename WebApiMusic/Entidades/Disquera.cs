using System.ComponentModel.DataAnnotations;
using WebApiMusic.Validaciones;

namespace WebApiMusic.Entidades
{
    public class Disquera
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<Music> Music { get; set; }

        public List<ArtistaDisquera> ArtistaDisquera { get; set; }
    }
}
