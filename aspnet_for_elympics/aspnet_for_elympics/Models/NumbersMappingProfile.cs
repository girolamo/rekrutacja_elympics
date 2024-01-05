using Aspnet_for_elympics.Entities;
using AutoMapper;

namespace Aspnet_for_elympics.Models
{
    public class NumbersMappingProfile : Profile
    {
        public NumbersMappingProfile()
        {
            CreateMap<RandomNumberDTO, Number>();
            CreateMap<Number, NumberDTO>();
        }
    }
}
