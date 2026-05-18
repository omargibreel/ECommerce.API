using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Domain.Models.ProductModule;
using Talabat.Shared.DTOs.ProductDTOs;

namespace Talabat.Services.Implementation.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(
                dest => dest.ProductBrand,
                opt => opt.MapFrom(
                    src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType,
                opt => opt.MapFrom(src => src.ProductType.Name));

            CreateMap<ProductBrand, BrandDTO>();

            CreateMap<ProductType, TypeDTO>();
        }
    }
}
