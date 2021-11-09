using MobileWeather.Models;
using MobileWeather.Views;
using Prism.Commands;
using Prism.Navigation;

namespace MobileWeather.ItemViewModels
{
    public class WeatherItemViewModel : WeatherResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectWeatherCommand;

        public WeatherItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectWeatherCommand =>
            _selectWeatherCommand ?? (_selectWeatherCommand = new DelegateCommand(SelectWeatherAsync));

        private async void SelectWeatherAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"weather",this}
            };

            await _navigationService.NavigateAsync(nameof(WeatherDetailPage), parameters);
        }
    }
}
