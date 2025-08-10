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
        CreateMap<Sale, SaleDto>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Items.Sum(i => i.UnitPrice * i.Quantity - i.Discount)))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItem, SaleItemDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));


        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Number, opt => opt.Ignore()) // será gerado
            .ForMember(dest => dest.Discount, opt => opt.Ignore()) // calculado
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        //products
        CreateMap<Product, ProductDto>().ReverseMap();

        //Branches
        CreateMap<Branch, BranchesDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}