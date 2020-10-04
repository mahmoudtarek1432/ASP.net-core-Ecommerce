using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AspIdentity.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;

        public OrderController(IOrderRepo orderRepo, IProductRepo productRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpGet("OrdersList")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> getUserOrders()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Orders> order = await _orderRepo.getOrdersbyUserId(user.Value);
            if(order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(order).Reverse());
            }
            return BadRequest();
        }

        [HttpGet("OrderDetails/{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> getUserOrderDetails(int orderId)
        {
            OrderDetails orderdetails = await _orderRepo.getOrderDetails(orderId);
            if (orderdetails != null)
            {
                var details = _mapper.Map<OrderDetailsDto>(orderdetails);
                details.Status = await _orderRepo.getOrderStatus(orderId);
                return Ok(details);
            }
            return BadRequest();
        }

        [HttpGet("OrderProducts/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderProductReadDto>>> getOrderProducts(int orderId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);
            var orderProducts = await _orderRepo.getOrderProducts(orderId);
            if (orderProducts != null)
            {
                var orderproducts = _mapper.Map<IEnumerable<OrderProductReadDto>>(orderProducts);
                foreach( var pro in orderproducts)
                {
                    var product = _productRepo.getProductById(pro.ProductId);
                    pro.Name = product.Name;
                    pro.image = product.ImagePathsArr[0];
                }
                
                return Ok(orderproducts);
            }
            return BadRequest();
        }

        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> createOrder([FromBody]OrderCreateDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                Orders order = _mapper.Map<Orders>(orderDto);
                order.OrderClientId = user.Value;

                //first product name & image
                int firstProductId = orderDto.products[0].ProductId;

                var result = await _orderRepo.CreateOrder(order,firstProductId); //either order number will be returned or a -1 on exception
                
                if(!result.IsSuccessful)
                {
                    return result;
                }

                int orderId = int.Parse(result.Message);

                var OrderDetails = _mapper.Map<OrderDetails>(orderDto.orderDetails);
                OrderDetails.DatePlaced = orderDto.DatePlaced;

                var orderProducts = _mapper.Map<IEnumerable<OrderProducts>>(orderDto.products);
                

                result = await _orderRepo.CreateOrder(OrderDetails, orderProducts, orderId);

                if(!result.IsSuccessful)
                {
                    return result;
                }

                return result;
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "The model is Invalid"
            };
        }
    }
}