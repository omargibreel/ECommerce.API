using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Models.ProductModule;
using ECommerce.Shared.DTOs.ProductDTOs;

namespace ECommerce.Services.Implementation.MappingProfiles
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
                opt => opt.MapFrom(src => src.ProductCategory.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandDTO>();

            CreateMap<ProductCategory, TypeDTO>();
        }
    }
}
