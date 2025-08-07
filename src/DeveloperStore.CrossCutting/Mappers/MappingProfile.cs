using AutoMapper;
using DeveloperStore.Application.Commands.Sales;
using DeveloperStore.Application.DTOs;
using DeveloperStore.Domain.Entities;

namespace DeveloperStore.CrossCutting.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateSaleItemDto, SaleItem>();
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Total, opt => opt.Ignore()) // Já é calculado na entidade
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}