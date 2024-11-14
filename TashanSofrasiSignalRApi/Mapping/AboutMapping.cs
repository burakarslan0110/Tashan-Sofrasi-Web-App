using AutoMapper;
using TashanSofrasi.DTOLayer.AboutDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class AboutMapping : Profile
    {
        public AboutMapping()
        {
            CreateMap<About,ResultAboutDTO>().ReverseMap();
            CreateMap<About,CreateAboutDTO>().ReverseMap();
            CreateMap<About,GetAboutDTO>().ReverseMap();
            CreateMap<About,UpdateAboutDTO>().ReverseMap();
        }
    }
}
