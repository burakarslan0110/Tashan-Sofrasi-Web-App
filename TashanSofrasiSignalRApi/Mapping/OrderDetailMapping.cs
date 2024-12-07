using AutoMapper;
using TashanSofrasi.DTOLayer.OrderDetailDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, CreateOrderDetailDTO>().ReverseMap();

        }
    }
}
