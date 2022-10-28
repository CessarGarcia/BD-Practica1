using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiMusic.Validaciones;

namespace WebApiMusic.Entidades
{
    public class Music //: IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] 
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener hasta 5 caracteres")]
        //[PrimeraLetraMayuscula]
        public string SongName { get; set; }
        [PrimeraLetraMayuscula]

        public string Artista { get; set; }
        public string Album { get; set; }
        public string Discografia { get; set; }
        public int ArtistaId { get; set; }
    }
}
