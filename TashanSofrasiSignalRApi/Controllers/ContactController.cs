using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DTOLayer.ContactDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListContact()
        {
            var values = _mapper.Map<List<ResultContactDTO>>(_contactService.TGetListAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDTO createContactDTO)
        {
            var newValue = _mapper.Map<Contact>(createContactDTO);
            _contactService.TAdd(newValue);
            return Ok("Mesajınız başarıyla gönderildi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var value = _contactService.TGetByID(id);
            _contactService.TDelete(value);
            return Ok("Mesajınız başarıyla silindi!");
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var value = _contactService.TGetByID(id);
            return Ok(value);
        }

    }
}
