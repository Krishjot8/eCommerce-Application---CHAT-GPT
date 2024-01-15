using AutoMapper;
using eCommerce_App.DTOs;
using eCommerce_App.Models;

namespace eCommerce_App.MappingProfiles
{
    public class ECommerceMappingProfile : Profile
    {

        public ECommerceMappingProfile()

        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductBrand, ProductBrandDto>();
            CreateMap<ProductBrandDto, ProductBrand>();

            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<ProductTypeDto, ProductType>();

        }

    }
}
