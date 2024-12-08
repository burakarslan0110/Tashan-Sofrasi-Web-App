using AutoMapper;
using TashanSofrasi.DTOLayer.OrderDetailDTOs;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Mapping
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, CreateOrderDetailDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();

        }
    }
}
