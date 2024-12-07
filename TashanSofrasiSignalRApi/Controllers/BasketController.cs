using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TashanSofrasi.BusinessLayer.Abstract;
using TashanSofrasi.DataAccessLayer.Concrete;
using TashanSofrasi.DTOLayer.BasketDTO;
using TashanSofrasi.DTOLayer.BookingDTO;
using TashanSofrasi.DTOLayer.OrderDetailDTO;
using TashanSofrasi.DTOLayer.OrderDTO;
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
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public BasketController(IBasketService basketService, IMapper mapper, IProductService productService, IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _basketService = basketService;
            _mapper = mapper;
            _productService = productService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
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
            createBasketDTO.MenuTableID = 1;
            var basket = _basketService.TGetBasketByProductID(createBasketDTO.ProductID, createBasketDTO.MenuTableID);

            if (basket != null)
            {
                int count = basket.Count + createBasketDTO.Count;
                _basketService.TUpdate(new Basket()
                {
                    BasketID = basket.BasketID,
                    MenuTableID = basket.MenuTableID,
                    ProductID = basket.ProductID,
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
                    Count = createBasketDTO.Count,
                    MenuTableID = createBasketDTO.MenuTableID,
                    Price = _productService.TGetProductPriceByProductID(createBasketDTO.ProductID),
                    TotalPrice = createBasketDTO.Count * _productService.TGetProductPriceByProductID(createBasketDTO.ProductID)
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

        [HttpPost("CompleteOrder/{menuTableId}")]
        public IActionResult CompleteOrder(int menuTableId)
        {
            var basket = _basketService.TGetBasketByMenuTableID(menuTableId);

            if (!basket.Any())
            {
                return BadRequest("Sepetinizde ürün bulunmamaktadır!");
            }

            var createorderDTO = new CreateOrderDTO()
            {
                MenuTableID = menuTableId,
                OrderDate = DateTime.Now.ToShortDateString(),
                OrderStatus = false,
                TotalPrice = basket.Sum(x => x.TotalPrice)
            };

            var order = _mapper.Map<Order>(createorderDTO);
            _orderService.TAdd(order);
            _orderService.TAddOrderAsync();


            foreach (var basketitem in basket)
            {
                var createOrderDetailDTO = new CreateOrderDetailDTO()
                {
                    OrderID = order.OrderID,
                    ProductID = basketitem.ProductID,
                    Count = basketitem.Count,
                    UnitPrice = basketitem.Price,
                    TotalPrice = basketitem.TotalPrice
                };
                _orderDetailService.TAdd(_mapper.Map<OrderDetail>(createOrderDetailDTO));
            }
            _orderDetailService.TAddOrderDetailAsync();

            _basketService.TClearBasket(basket);

            return Ok("Siparişiniz başarıyla alındı!");
        }
    }
}
