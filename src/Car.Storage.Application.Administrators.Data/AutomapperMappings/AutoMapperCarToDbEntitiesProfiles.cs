using AutoMapper;
using Car.Storage.Application.Administrators.Domain.Entities;

namespace Car.Storage.Application.Administrators.Data.AutomapperMappings
{
    public class AutoMapperCarToDbEntitiesProfiles : Profile
    {
        public AutoMapperCarToDbEntitiesProfiles()
        {
            CreateMap<Domain.Entities.Car,Repositories.Entities.Car>().ReverseMap();    

            CreateMap<CarOwner, Repositories.Entities.CarOwner>()
           .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.IdentityDocument.DocumentNumber))
           .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.IdentityDocument.DocumentType))
           .ForMember(dest => dest.DocumentExpiryDate, opt => opt.MapFrom(src => src.IdentityDocument.DocumentExpiryDate))
           .ForMember(dest => dest.DocumentIssuingAuthority, opt => opt.MapFrom(src => src.IdentityDocument.DocumentIssuingAuthority))
           .ForMember(dest => dest.DocumentDateOfCreation, opt => opt.MapFrom(src => src.IdentityDocument.DocumentDateOfCreation)).ReverseMap();
        }
    }
}
