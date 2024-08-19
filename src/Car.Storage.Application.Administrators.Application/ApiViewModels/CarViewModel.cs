using Car.Storage.Application.Administrators.Application.FluentValidators;
using Car.Storage.Application.Administrators.Domain.FluentValidators;

namespace Car.Storage.Application.Administrators.Application.ApiViewModels
{
    public class CarViewModel : BaseViewModel.BaseViewModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public bool IsRunning { get; set; }
        public bool IsNew { get; set; }
        public bool IsForSale { get; set; }
        public decimal Price { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string? CarPlate { get; set; }
        public CarOwnerViewModel? Owner { get; set; }

    }
}
