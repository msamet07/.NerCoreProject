using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workintech02RestApiDemo.Domain.Dto
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Towns { get; set; }
    }

    public class  CityDtoProfile : Profile
    {
        public CityDtoProfile()
        {
            CreateMap<Workintech02RestApiDemo.Domain.Entities.City, CityDto>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.Towns, opt => opt.MapFrom(source => string.Join(",", source.Towns.Select(x => x.Name).ToList())))
                ;
            CreateMap<CityDto, Workintech02RestApiDemo.Domain.Entities.City>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.CityId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.CityName))
                ;
        }
    }
}
