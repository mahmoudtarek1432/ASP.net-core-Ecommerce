using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //source -> destination
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, MinifiedProductReadDto>();
        }
    }
}
