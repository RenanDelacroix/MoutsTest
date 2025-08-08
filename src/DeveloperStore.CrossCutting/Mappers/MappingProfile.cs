using AutoMapper;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Domain.Entities;

namespace DeveloperStore.CrossCutting.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Sales
        CreateMap<CreateSaleItemDto, SaleItem>();
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Number, opt => opt.Ignore()) // será gerado
            .ForMember(dest => dest.Discount, opt => opt.Ignore()) // calculado
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        //products
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}