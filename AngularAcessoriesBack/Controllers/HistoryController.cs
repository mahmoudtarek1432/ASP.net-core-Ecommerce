using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AngularAcessoriesBack.Services;
using AspIdentity.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly UserManager<CustomIdentityUser> _UserManager;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public HistoryController(UserManager<CustomIdentityUser> userManager,IProductRepo productRepo,IMapper mapper, IClientService clientService)
        {
            _UserManager = userManager;
            _productRepo = productRepo;
            _mapper = mapper;
            _clientService = clientService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MinifiedProductReadDto>>> GetHistoryItems()
        {
            if(ModelState.IsValid)
            {
                var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _UserManager.FindByIdAsync(userid);
                if(user != null)
                {
                    string[] recentlyViewdProducts = user.RecentlyViewedArr;
                    var list = new List<Product>();
                    foreach (var pro in recentlyViewdProducts)
                    {
                        list.Add(_productRepo.getProductById(int.Parse(pro)));
                    }

                    return Ok(_mapper.Map<IEnumerable<MinifiedProductReadDto>>(list).Reverse());
                }
                return NotFound();
            }
            return BadRequest();
        }


        [HttpPost("AddRecentlyViewed")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> addRecentlyViewed([FromBody] int productId)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _clientService.AddToRecentlyViewed(user.Value, productId);
                return result;
            }
            return BadRequest("Bad Model State");
        }
    }
}