using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.FeatureDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateFeatureDTO> _updateFeatureValidator;

		public FeatureController(IFeatureService featureService, IMapper mapper, IValidator<UpdateFeatureDTO> updateFeaturevalidator)
        {
            _featureService = featureService;
            _mapper = mapper;
			_updateFeatureValidator = updateFeaturevalidator;
		}

        [HttpGet]
        public IActionResult ListFeature()
        {
            var values = _mapper.Map<List<ResultFeatureDTO>>(_featureService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            var newValue = _mapper.Map<Feature>(createFeatureDTO);
            _featureService.TAdd(newValue);
            return Ok("Yeni öne çıkan kaydı başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var value = _featureService.TGetByID(id);
            _featureService.TDelete(value);
            return Ok("Öne çıkan kaydı başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDTO updateFeatureDTO)
        {
            var validatorResult = _updateFeatureValidator.Validate(updateFeatureDTO);
			if (!validatorResult.IsValid)
			{
				return BadRequest(validatorResult.Errors);
			}
			var newValue = _mapper.Map<Feature>(updateFeatureDTO);
            _featureService.TUpdate(newValue);
            return Ok("Öne çıkan kaydı başarıyla güncellendi");
        }

        [HttpGet("{id}")]
        public IActionResult GetFeature(int id)
        {
            var value = _featureService.TGetByID(id);
            return Ok(value);
        }
    }
}
