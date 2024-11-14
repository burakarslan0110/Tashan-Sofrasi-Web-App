using AutoMapper;
using TashanSofrasi.DTOLayer.MenuTableDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class MenuTableMapping : Profile
    {
        public MenuTableMapping() 
        {
            CreateMap<MenuTable, CreateMenuTableDTO>().ReverseMap();
            CreateMap<MenuTable, ResultMenuTableDTO>().ReverseMap();
            CreateMap<MenuTable, GetMenuTableDTO>().ReverseMap();
            CreateMap<MenuTable, UpdateMenuTableDTO>().ReverseMap();
        }
    }
}
