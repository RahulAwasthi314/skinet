using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember (d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember (d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember (d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
        }
    }
}