using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.DiscountDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListDiscount()
        {
            var values = _mapper.Map<List<ResultDiscountDTO>>(_discountService.TGetListAll());
            return Ok (values);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDTO createDiscountDTO)
        {
            var newValue = _mapper.Map<Discount>(createDiscountDTO);
            _discountService.TAdd(newValue);
            return Ok("Yeni indirim kaydı başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
           var value = _discountService.TGetByID(id);
           _discountService.TDelete(value);
            return Ok("İndirim kaydı başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDTO updateDiscountDTO)
        {
            var newValue = _mapper.Map<Discount>(updateDiscountDTO);
            _discountService.TUpdate(newValue);
            return Ok("İndirim kaydı başarıyla güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetDiscount(int id)
        {
            var value = _discountService.TGetByID(id);
            return Ok(value);
        }
    }
}
