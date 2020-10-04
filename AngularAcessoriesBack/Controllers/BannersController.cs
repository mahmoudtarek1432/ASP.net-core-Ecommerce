using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace AngularAcessoriesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerRepo _repo;
        private readonly IMapper _mapper;

        public BannersController(IBannerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<BannerReadDto> getOnDisplayBanners()
        {
            var banners = _repo.getOnDisplayBanners();
            if( banners != null)
            {
                return Ok(_mapper.Map<BannerReadDto>(banners));
            }
            return NotFound();
        }

        [HttpPost]
        public void createNewBanner(BannerCreateDto newbanner)
        {
            var banner = _mapper.Map<Banner>(newbanner);
            _repo.createNewBanner(banner);
            _repo.saveChanges();
        }


    }
}
