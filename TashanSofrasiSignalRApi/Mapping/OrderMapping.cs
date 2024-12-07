using AutoMapper;
using TashanSofrasi.DTOLayer.OrderDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
        }
    }
}
