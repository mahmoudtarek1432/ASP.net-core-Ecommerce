using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AngularAcessoriesBack.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController: ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IClientService _clientservice;

        public ReviewsController(IReviewRepo reviewRepo, IMapper mapper, IClientService clientService)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _clientservice = clientService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> createReview(ReviewCreateDto reviewCreateDto)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Review = _mapper.Map<Review>(reviewCreateDto);
            Review.Name = await _clientservice.AddUserName(UserId);
            _reviewRepo.CreateProductReview(UserId, Review);
            _reviewRepo.saveChanges();

            return Ok();
        }
    }
}


