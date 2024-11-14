using AutoMapper;
using TashanSofrasi.DTOLayer.FooterDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class FooterMapping : Profile
    {
        public FooterMapping()
        {
            CreateMap<Footer, ResultFooterDTO>().ReverseMap();
            CreateMap<Footer, CreateFooterDTO>().ReverseMap();
            CreateMap<Footer, GetFooterDTO>().ReverseMap();
            CreateMap<Footer, UpdateFooterDTO>().ReverseMap();
        }
    }
}
