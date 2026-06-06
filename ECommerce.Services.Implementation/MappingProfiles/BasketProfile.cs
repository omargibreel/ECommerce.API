using AutoMapper;
using ECommerce.Domain.Models.CartModule;
using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Implementation.MappingProfiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDTO, CustomerBasket>().ReverseMap();

            CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
        }
    }
}
