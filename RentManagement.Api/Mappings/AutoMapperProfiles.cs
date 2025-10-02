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
                .ForMember(dest => dest.CurrentAgreementId,
                    opt => opt.MapFrom(src => src.CurrentAgreement != null ? src.CurrentAgreement.Id : (int?)null))
                .ForMember(dest => dest.CurrentShopNumber,
                    opt => opt.MapFrom(src => src.CurrentAgreement != null ? src.CurrentAgreement.Shop.ShopNumber : null));

            CreateMap<Tenant, TenantCreateDto>().ReverseMap();

            CreateMap<RentalAgreement, RentalAgreementDto>()
                .ForMember(dest => dest.ShopNumber, opt => opt.MapFrom(src => src.Shop.ShopNumber))
                .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.Tenant.Name));
            CreateMap<RentalAgreement, RentalAgreementCreateDto>().ReverseMap();
        }
    }
}
