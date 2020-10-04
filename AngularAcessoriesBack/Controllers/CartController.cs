using System;
using System.Collections.Generic;
using System.Data.Sql;
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
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _CartRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepo _ProductRepo;

        public CartController(IProductRepo productRepo, ICartRepo cartrepo, IMapper mapper)
        {
            _CartRepo = cartrepo;
            _mapper = mapper;
            _ProductRepo = productRepo;
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<ActionResult<UserManagerResponse>> addToUserCart([FromBody] AddProductToCart product)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(User != null)
            {
                int AvailableQuantity = _ProductRepo.getProductQuantity(product.ProductId);
                if(AvailableQuantity >= product.InCartQuantity)
                {
                    var cartItem = _mapper.Map<CartItem>(product);
                    var info = _ProductRepo.getProductById(cartItem.ProductId);
                    cartItem.UserId = userId;
                    cartItem.ItemName = info.Name;
                    cartItem.Image = info.ImagePathsArr[0];
                    cartItem.UnitePrice = info.Price;
                    var result = _CartRepo.AddToCartQuantity(cartItem, AvailableQuantity);
                    if (!result.IsSuccessful)
                    {
                        await _CartRepo.addToUserCart(cartItem);
                        _CartRepo.saveContext();
                        return new UserManagerResponse
                        {
                            IsSuccessful = true,
                            Message = "Added to cart"
                        };
                    }
                    _CartRepo.saveContext();
                    return result;
                }
                return new UserManagerResponse
                {
                    IsSuccessful = false,
                    Message = "Desired Quantity is over the available"
                };
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "Process not successful"
            };
        }

        [HttpGet("")]
        public async Task<ActionResult<JsonManagerResponse<IEnumerable<CartItemReadDto>>>> GetCartItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (User != null)
            {
                var cartItems = _CartRepo.getUserCart(userId);
                var list = _mapper.Map<IEnumerable<CartItemReadDto>>(cartItems);
              
                return Ok(new JsonManagerResponse<IEnumerable<CartItemReadDto>> {
                    IsSuccessful = true,
                    ResponseObject = _mapper.Map<IEnumerable<CartItemReadDto>>(cartItems)
                });
            }
            return NotFound(new JsonManagerResponse<CartItemReadDto>
            {
                IsSuccessful = false,
                Message = "Process faild",
                ResponseObject = null
            });
        }

        [HttpPost("Remove/{productId}")]
     
        public async Task<ActionResult<UserManagerResponse>> removeFromCart(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (User != null)
            {
                var result = _CartRepo.removeItem(userId, productId);
                return result;
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = ""
            };
        }

        [HttpPost("Update/{productId}/{updatedQuantity}")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> UpdateItem(int productId, int updatedQuantity)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (User != null)
            {
                var item = _ProductRepo.getProductById(productId);
                var cartItem = _CartRepo.getUserCart(userId).Where(c => c.ProductId == productId).FirstOrDefault();
                if(cartItem != null)
                {
                    var result = _CartRepo.UpdateCartItem(productId, userId, updatedQuantity, item.QuantityAvailable);
                    _CartRepo.saveContext();
                    return result;
                }
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = ""
            };
        }
    }
}