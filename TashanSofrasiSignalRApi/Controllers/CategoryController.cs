using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.CategoryDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDTO> _createCategoryvalidator;
        private readonly IValidator<UpdateCategoryDTO> _updateCategoryvalidator;

		public CategoryController(ICategoryService categoryService, IMapper mapper, IValidator<CreateCategoryDTO> createCategoryvalidator, IValidator<UpdateCategoryDTO> updateCategoryvalidator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
			_createCategoryvalidator = createCategoryvalidator;
			_updateCategoryvalidator = updateCategoryvalidator;
		}

        [HttpGet]
        public IActionResult ListCategory()
        {
            var values = _mapper.Map<List<ResultCategoryDTO>>(_categoryService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
			var validatorResult = _createCategoryvalidator.Validate(createCategoryDTO);
			if (!validatorResult.IsValid)
			{
				return BadRequest(validatorResult.Errors);
			}
			var newValue = _mapper.Map<Category>(createCategoryDTO);
            _categoryService.TAdd(newValue);
            return Ok("Kategori başarıyla eklendi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            _categoryService.TDelete(value);
            return Ok("Kategori başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
			var validatorResult = _updateCategoryvalidator.Validate(updateCategoryDTO);
			if (!validatorResult.IsValid)
			{
				return BadRequest(validatorResult.Errors);
			}
			var newValue = _mapper.Map<Category>(updateCategoryDTO);
            _categoryService.TUpdate(newValue);
            return Ok("Kategori başarıyla güncellendi!");
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var value = _categoryService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("CategoryCount")]
        public IActionResult CategoryCount()
        {
            return Ok(_categoryService.TCategoryCount());
        }

        [HttpGet("ActiveCategoryCount")]
        public IActionResult ActiveCategoryCount()
        {
            return Ok(_categoryService.TActiveCategoryCount());
        }

        [HttpGet("PassiveCategoryCount")]
        public IActionResult PassiveCategoryCount()
        {
            return Ok(_categoryService.TPassiveCategoryCount());
        }

        [HttpPut("CategoryStatusChangeToFalse/{id}")]
        public IActionResult CategoryStatusChangeToFalse(int id)
        {
            _categoryService.TCategoryStatusChangeToFalse(id);
            return Ok("Kategori başarıyla pasif hale getirildi!");
        }

        [HttpPut("CategoryStatusChangeToTrue/{id}")]
        public IActionResult CategoryStatusChangeToTrue(int id)
        {
            _categoryService.TCategoryStatusChangeToTrue(id);
            return Ok("Kategori başarıyla aktif hale getirildi!");
        }
    }
}
