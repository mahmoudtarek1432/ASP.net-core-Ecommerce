using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            //source -> destination
            CreateMap<ReviewCreateDto, Review>();
        }  
    }
}
