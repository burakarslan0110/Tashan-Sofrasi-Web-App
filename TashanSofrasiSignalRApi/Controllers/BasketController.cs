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
        private readonly IProductService _productService;

        public BasketController(IBasketService basketService, IMapper mapper, IProductService productService)
        {
            _basketService = basketService;
            _mapper = mapper;
            _productService = productService;
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
            createBasketDTO.MenuTableID = 4;
            var basket = _basketService.TGetBasketByProductID(createBasketDTO.ProductID, createBasketDTO.MenuTableID);

            if (basket != null)
            {
                var count = basket.Count;
                count++;
                _basketService.TUpdate(new Basket()
                {
                    BasketID = basket.BasketID,
                    MenuTableID = basket.MenuTableID,
                    ProductID = createBasketDTO.ProductID,
                    Count = count,
                    Price = _productService.TGetProductPriceByProductID(createBasketDTO.ProductID),
                    TotalPrice = count * _productService.TGetProductPriceByProductID(createBasketDTO.ProductID)
                });
            }

            else
            {
                _basketService.TAdd(new Basket()
                {
                    ProductID = createBasketDTO.ProductID,
                    Count = 1,
                    MenuTableID = createBasketDTO.MenuTableID,
                    Price = _productService.TGetProductPriceByProductID(createBasketDTO.ProductID),
                    TotalPrice = _productService.TGetProductPriceByProductID(createBasketDTO.ProductID)
                });
            }
            return Ok("Ürün sepete eklendi!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetByID(id);
            _basketService.TDelete(value);
            return Ok("Ürün sepetten silindi!");
        }
    }
}
