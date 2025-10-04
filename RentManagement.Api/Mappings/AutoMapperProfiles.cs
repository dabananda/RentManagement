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
            CreateMap<Shop, ShopDetailsDto>().ReverseMap();

            CreateMap<Tenant, TenantCreateDto>().ReverseMap();
            CreateMap<Tenant, TenantDetailsDto>().ReverseMap();
            CreateMap<Tenant, TenantDto>()
                .ForMember(dest => dest.Agreements,
                                opt => opt.MapFrom(src =>
                                    src.RentalAgreements.Where(a => a.IsActive)))
                .ReverseMap();

            CreateMap<RentalAgreement, RentalAgreementCreateDto>().ReverseMap();
            CreateMap<RentalAgreement, RentalAgreementDto>()
                .ForMember(dest => dest.Shop, opt => opt.MapFrom(src => src.Shop))
                .ForMember(dest => dest.Tenant, opt => opt.MapFrom(src => src.Tenant))
                .ReverseMap();
            CreateMap<RentalAgreement, AgreementDetailsDto>()
                .ForMember(dest => dest.Shop, opt => opt.MapFrom(src => src.Shop))
                .ForMember(dest => dest.Tenant, opt => opt.MapFrom(src => src.Tenant))
                .ReverseMap();
            CreateMap<RentalAgreement, AgreementWithShopDto>()
                .ForMember(dest => dest.Shop, opt => opt.MapFrom(src => src.Shop))
                .ReverseMap();
        }
    }
}
