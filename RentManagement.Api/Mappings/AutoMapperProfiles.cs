using AutoMapper;
using RentManagement.Api.DTOs;
using RentManagement.Api.Models;

namespace RentManagement.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Shop, ShopDto>().ReverseMap();
            CreateMap<Shop, ShopCreateDto>().ReverseMap();

            CreateMap<Tenant, TenantDto>()
                .ForMember(dest => dest.ActiveAgreementIds,
                    opt => opt.MapFrom(src => src.RentalAgreements
                        .Where(a => a.IsActive)
                        .Select(a => a.Id)
                        .ToList()))
                .ForMember(dest => dest.ActiveShopNumbers,
                    opt => opt.MapFrom(src => src.RentalAgreements
                        .Where(a => a.IsActive)
                        .Select(a => a.Shop.ShopNumber)
                        .ToList()));

            CreateMap<Tenant, TenantCreateDto>().ReverseMap();

            CreateMap<RentalAgreement, RentalAgreementDto>()
                .ForMember(dest => dest.ShopNumber, opt => opt.MapFrom(src => src.Shop.ShopNumber))
                .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Tenant.Name));
            CreateMap<RentalAgreement, RentalAgreementCreateDto>().ReverseMap();
        }
    }
}
