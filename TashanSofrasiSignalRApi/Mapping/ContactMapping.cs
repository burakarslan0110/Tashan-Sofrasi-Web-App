using AutoMapper;
using TashanSofrasi.DTOLayer.ContactDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, CreateContactDTO>().ReverseMap();
            CreateMap<Contact, ResultContactDTO>().ReverseMap();
            CreateMap<Contact, GetContactDTO>().ReverseMap();
        }
    }
}
