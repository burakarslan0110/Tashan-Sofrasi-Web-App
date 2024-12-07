using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.AboutDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateAboutDTO> _validator;
        public AboutController(IAboutService aboutService, IMapper mapper, IValidator<UpdateAboutDTO> validator)
        {
            _aboutService = aboutService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult ListAbout()
        {
            var values = _mapper.Map<List<ResultAboutDTO>>(_aboutService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDTO createAboutDTO)
        {
            var newValue= _mapper.Map<About>(createAboutDTO);
            _aboutService.TAdd(newValue);
            return Ok("Hakkımda kısmı başarıyla eklendi!");
        }

        [HttpDelete]
        public IActionResult DeleteAbout(int id)
        {
            var value = _aboutService.TGetByID(id);
            _aboutService.TDelete(value);
            return Ok("Hakkımda kısmı başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            var validatorResult = _validator.Validate(updateAboutDTO);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            var newValue = _mapper.Map<About>(updateAboutDTO);
            _aboutService.TUpdate(newValue);
            return Ok("Hakkımda kısmı başarıyla güncellendi!");
        }

        [HttpGet("{id}")]
        public IActionResult GetAbout(int id)
        {
            var value = _aboutService.TGetByID(id);
            return Ok(value);
        }
    }
}
