using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.MenuTableDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTableController : ControllerBase
    {
        private readonly IMenuTableService _menuTableService;
        private readonly IMapper _mapper;

        public MenuTableController(IMenuTableService menuTableService, IMapper mapper)
        {
            _menuTableService = menuTableService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListMenuTable()
        {
            var values = _mapper.Map<List<ResultMenuTableDTO>>(_menuTableService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMenuTable(CreateMenuTableDTO createMenuTableDTO)
        {
            var newValue = _mapper.Map<MenuTable>(createMenuTableDTO);
            _menuTableService.TAdd(newValue);
            return Ok("Masa kaydı başarıyla eklendi!");
        }

        [HttpPost("{id}")]
        public IActionResult DeleteMenuTable(int id)
        {
            var value = _menuTableService.TGetByID(id);
            _menuTableService.TDelete(value);
            return Ok("Masa kaydı başarıyla silindi!");
        }

        [HttpPut]
        public IActionResult UpdateMenuTable(UpdateMenuTableDTO updateMenuTableDTO)
        {
            var newValue = _mapper.Map<MenuTable>(updateMenuTableDTO);
            _menuTableService.TUpdate(newValue);
            return Ok("Masa kaydı başarıyla güncellendi!");
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuTable(int id)
        {
            var value = _menuTableService.TGetByID(id);
            return Ok(value);
        }

        [HttpGet("CountMenuTable")]
        public IActionResult CountMenuTable()
        {
            return Ok(_menuTableService.TMenuTableCount());
        }
    }
}
