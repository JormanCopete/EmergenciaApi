using AutoMapper;
using Polly.Core.DTOs.ML;
using Polly.Core.Entities.ML;

//https://medium.com/@cdelgado1978/implementando-automapper-sin-perderte-en-el-camino-213e11af72c1

namespace Polly.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<emergencia_resumen, emergencia_resumenDto>().ReverseMap();
            CreateMap<emergencia_detalle, emergencia_detalleDto>().ReverseMap();
        }
    }
}
