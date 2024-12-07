using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.FooterDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly IFooterService _footerService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateFooterDTO> _footerValidator;

		public FooterController(IFooterService footerService, IMapper mapper, IValidator<UpdateFooterDTO> footerValidator)
        {
            _footerService = footerService;
            _mapper = mapper;
			_footerValidator = footerValidator;
		}

        [HttpGet]
        public IActionResult ListFooter()
        {
            var values = _mapper.Map<List<ResultFooterDTO>>(_footerService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateFooter(CreateFooterDTO createFooterDTO)
        {
            var newValue = _mapper.Map<Footer>(createFooterDTO);
            _footerService.TUpdate(newValue);
            return Ok("Yeni footer kaydı başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFooter(int id)
        {
            var value = _footerService.TGetByID(id);
            _footerService.TDelete(value);
            return Ok("Footer kaydı başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateFooter(UpdateFooterDTO updateFooterDTO)
        {
            var validatorResponse = _footerValidator.Validate(updateFooterDTO);
			if (!validatorResponse.IsValid)
			{
				return BadRequest(validatorResponse.Errors);
			}
			var newValue = _mapper.Map<Footer>(updateFooterDTO);
            _footerService.TUpdate(newValue);
            return Ok("Footer kaydı başarıyla güncellendi!");
        }

        [HttpGet("{id}")]
        public IActionResult GetFooter(int id)
        {
            var value = _footerService.TGetByID(id);
            return Ok(value);
        }
    }
}
