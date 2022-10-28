using System.ComponentModel.DataAnnotations;
using WebApiMusic.Validaciones;

namespace WebApiMusic.Entidades
{
    public class Artista
    {
        public int Id { get; set; }

        //PrimeraLetraMayuscula
         [Required(ErrorMessage = "El primer campo es necesario")]
         [StringLength(maximumLength:15, ErrorMessage ="El campo 0 solo puede tener hasta 5 caracteres")]
        //[PrimeraLetraMayuscula]
        public string NombreArtistico { get; set; }
            
        public string NombreReal {get; set; }
        public string Entidad {get; set; }

        public int Edad { get; set;  }
    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(NombreArtistico))
            {
                var primeraLetra = NombreArtistico[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new String[] { nameof(NombreArtistico) });
                }
            }
        }
        public List<Music> Musics { get; set; }
    }
}
