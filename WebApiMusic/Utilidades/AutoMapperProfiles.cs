using AutoMapper;
using WebApiMusic.DTOs;
using WebApiMusic.Entidades;
namespace WebApiMusic.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArtistaDTO, Artista>();
            CreateMap<Artista, GetArtistaDTO>();
            CreateMap<Artista, ArtistaConDisqueraDTO>()
                .ForMember(artistaDTO => artistaDTO.Disquera, opciones => opciones.MapFrom(MapAlumnoDTOClases));
            CreateMap<DisqueraCreacionDTO, Disquera>()
                .ForMember(disquera => disquera.ArtistaDisquera, opciones => opciones.MapFrom(MapAlumnoClase));
            CreateMap<Disquera, DisqueraDTO>();
            CreateMap<Disquera, DisqueraConArtistaDTO>()
                .ForMember(disqueraDTO => disqueraDTO.Artista, opciones => opciones.MapFrom(MapClaseDTOAlumnos));
            CreateMap<MusicCreacionDTO, Music>();
            CreateMap<Music, MusicDTO>();
        }

        private List<DisqueraDTO> MapAlumnoDTOClases(Artista alumno, GetArtistaDTO getAlumnoDTO)
        {
            var result = new List<DisqueraDTO>();

            if (alumno.ArtistaDisquera == null) { return result; }

            foreach (var alumnoClase in alumno.ArtistaDisquera)
            {
                result.Add(new DisqueraDTO()
                {
                    Id = alumnoClase.DisqueraId,
                    Name = alumnoClase.Disquera.Name
                });
            }

            return result;
        }

        private List<GetArtistaDTO> MapClaseDTOAlumnos(Disquera clase, DisqueraDTO claseDTO)
        {
            var result = new List<GetArtistaDTO>();

            if (clase.ArtistaDisquera == null)
            {
                return result;
            }

            foreach (var alumnoclase in clase.ArtistaDisquera)
            {
                result.Add(new GetArtistaDTO()
                {
                    Id = alumnoclase.ArtistaId,
                    NombreArtistico = alumnoclase.Artista.NombreArtistico,
                    NombreReal = alumnoclase.Artista.NombreReal,
                    Entidad = alumnoclase.Artista.Entidad,
                    Edad = alumnoclase.Artista.Edad 

                });
            }

            return result;
        }

        private List<ArtistaDisquera> MapAlumnoClase(DisqueraCreacionDTO claseCreacionDTO, Disquera clase)
        {
            var resultado = new List<ArtistaDisquera>();

            if (claseCreacionDTO.ArtistasIds == null) { return resultado; }
            foreach (var alumnoId in claseCreacionDTO.ArtistasIds)
            {
                resultado.Add(new ArtistaDisquera() { ArtistaId = alumnoId });
            }
            return resultado;
        }
    }
}
