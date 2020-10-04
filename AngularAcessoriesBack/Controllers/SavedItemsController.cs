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
    public class SavedItemsController : ControllerBase
    {
        private readonly IClientService _ClientService;
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _UserManager;
        private readonly IProductRepo _productRepo;

        public SavedItemsController(IClientService ClientService, IMapper mapper, UserManager<CustomIdentityUser> userManager, IProductRepo productRepo)
        {
            _ClientService = ClientService;
            _mapper = mapper;
            _UserManager = userManager;
            _productRepo = productRepo;
        }


        [HttpPost("AddSavedItem")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> addSavedItem([FromBody] int ProductId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _ClientService.AddToSavedItems(userId.Value, ProductId);

                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "The properties are not valid"
            };
        }

        [HttpPost("RemoveSavedItem")]
        [Authorize]
        public async Task<ActionResult<UserManagerResponse>> removeSavedItem([FromBody] int ProductId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var result = await _ClientService.RemoveSavedItem(userId.Value, ProductId);

                if (result.IsSuccessful)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return new UserManagerResponse
            {
                IsSuccessful = false,
                Message = "The properties are not valid"
            };
        }

        [HttpGet("")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MinifiedProductReadDto>>> getSavedItems()
        {
            var user = await _UserManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var saveditems = user.SavedItemsArr;
            List<Product> savedProducts = new List<Product>();
            if (saveditems.Length > 0)
            {
                foreach (var id in saveditems)
                {
                    var pro = _productRepo.getProductById(int.Parse(id));
                    if (pro != null)
                    {
                        savedProducts.Add(pro);
                    }
                }

                if (savedProducts.Count > 0)
                {
                    return Ok(_mapper.Map<IEnumerable<MinifiedProductReadDto>>(savedProducts));
                }
            }
            return NotFound("no saved items");
        }
    }
}