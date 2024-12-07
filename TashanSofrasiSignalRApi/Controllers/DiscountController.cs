using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<UpdateDiscountDTO> _validator;

        public DiscountController(IDiscountService discountService, IMapper mapper, IValidator<UpdateDiscountDTO> validator)
        {
            _discountService = discountService;
            _mapper = mapper;
            _validator = validator;
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
            var validatorResult = _validator.Validate(updateDiscountDTO);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
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

		[HttpPut("ChangeDiscountStatusToFalse/{id}")]
		public IActionResult ChangeDiscountStatusToFalse(int id)
		{
			_discountService.TChangeDiscountStatusToFalse(id);
			return Ok("İndirim başarıyla pasif hale getirildi!");
		}

		[HttpPut("ChangeDiscountStatusToTrue/{id}")]
		public IActionResult ChangeDiscountStatusToTrue(int id)
		{
			_discountService.TChangeDiscountStatusToTrue(id);
			return Ok("İndirim başarıyla aktif hale getirildi!");
		}
	}
}
