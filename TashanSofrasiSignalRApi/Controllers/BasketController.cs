using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.DTOLayer.BasketDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasiSignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBasketByMenuTableID(int id)
        {
            var values = _basketService.TGetBasketByMenuTableID(id);
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDTO createBasketDTO)
        {
            using var context = new TashanSofrasiContext();
            _basketService.TAdd(new Basket()
            {
                ProductID = createBasketDTO.ProductID,
                Count = 1,
                MenuTableID = 4,
                Price = context.Products.Where(x => x.ProductID == createBasketDTO.ProductID).Select(y => y.ProductPrice).FirstOrDefault(),
                TotalPrice = 0
            });
            return Ok("Ürün sepete eklendi!");
        }
    }
}
