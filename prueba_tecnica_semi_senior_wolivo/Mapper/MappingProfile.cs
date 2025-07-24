using AutoMapper;
using prueba_tecnica_semi_senior_wolivo.Dtos;
using prueba_tecnica_semi_senior_wolivo.Models;

namespace prueba_tecnica_semi_senior_wolivo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<ProyectoDto, Proyecto>().ReverseMap();
            CreateMap<ActividadDto, Actividad>().ReverseMap();
        }
    }

}
