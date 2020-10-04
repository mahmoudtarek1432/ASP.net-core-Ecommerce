using AngularAcessoriesBack.Dtos;
using AngularAcessoriesBack.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Profiles
{
    public class OrderProfiles: Profile
    {
        public OrderProfiles()
        {
            CreateMap<Orders, OrderReadDto>();
            CreateMap<OrderDetails, OrderDetailsDto>();
            CreateMap<OrderProducts, OrderProductReadDto>();
            CreateMap<OrderDetailsDto, OrderDetails>();
            CreateMap<OrderProductCreateDto, OrderProducts>();
            CreateMap<OrderCreateDto, Orders>();
        }
    }
}
