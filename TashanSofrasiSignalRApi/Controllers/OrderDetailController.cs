using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.OrderDetailDTOs;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }

        [HttpGet("{menuTableID}")]
        public IActionResult GetAllOrderDetailsByMenuTableID(int menuTableID)
        {
            var values = _mapper.Map<List<OrderDetailDTO>>(_orderDetailService.TGetAllOrderDetailsByMenuTableID(menuTableID));
            return Ok(values);
        }
    }
}
