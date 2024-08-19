using AutoMapper;
using Car.Storage.Application.Administrators.Application.ApiViewModels;

namespace Car.Storage.Application.Administrators.Application.AutomapperMappings
{
    public class AutoMapperCarViewModelsProfiles :Profile
    {
        public AutoMapperCarViewModelsProfiles()
        {

            this.CarViewModelMappings();
        }

        private void CarViewModelMappings()
        {
            CreateMap<Domain.Entities.Car, CarViewModel>()
            .ForMember(dest => dest.ValidationResult, opt => opt.MapFrom(src => src.ValidationResult)).ReverseMap();
  

        }

    }
}
