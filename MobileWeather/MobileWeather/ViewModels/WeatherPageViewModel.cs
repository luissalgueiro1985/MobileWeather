using MobileWeather.Helpers;
using MobileWeather.ItemViewModels;
using MobileWeather.Models;
using MobileWeather.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileWeather.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAPIService _apiService;
        private ObservableCollection<WeatherItemViewModel> _weatherList = new ObservableCollection<WeatherItemViewModel>();
        private List<string> _citiesList = new List<string> { "Lisbon", "Porto", "Madrid", "Barcelona", "London", "Dublin", "Brussels", "Berlin", "Paris", "Rome" };
        private bool _isRunning;
        private string _search;
        private List<WeatherResponse> _myWeatherList;
        private DelegateCommand _searchCommand;



        public WeatherPageViewModel(
            INavigationService navigationService,
            IAPIService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.CurrentWeather;
            LoadWeatherAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCities));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCities();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<WeatherItemViewModel> WeatherList
        {
            get => _weatherList;
            set => SetProperty(ref _weatherList, value);
        }


        private async void LoadWeatherAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                });
                return;
            }

            IsRunning = true;

            var list = new List<WeatherResponse>();

            foreach (string city in _citiesList)
            {
                Response response = await _apiService.GetWeather<WeatherResponse>(city);

                if (!response.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                    return;
                }

                list.Add((WeatherResponse)response.Result);
                
            }

            IsRunning = false;

            _myWeatherList = new List<WeatherResponse>(list);
            ShowCities();
        }

        private void ShowCities()
        {
            if (string.IsNullOrEmpty(Search))
            {
                WeatherList = new ObservableCollection<WeatherItemViewModel>(_myWeatherList.Select(w =>
                new WeatherItemViewModel(_navigationService) {
                    Id = w.Id,
                    Base = w.Base,
                    Cod = w.Cod,
                    Coord = w.Coord,
                    Dt = w.Dt,
                    Clouds = w.Clouds,
                    Main = w.Main,
                    Name = w.Name,
                    Sys = w.Sys,
                    Timezone = w.Timezone,
                    Visibility = w.Visibility,
                    Weather = w.Weather,
                    Wind = w.Wind
                }).ToList());
            }
            else
            {
                WeatherList = new ObservableCollection<WeatherItemViewModel>(
                    _myWeatherList.Select(w =>
                    new WeatherItemViewModel(_navigationService)
                    {
                        Id = w.Id,
                        Base = w.Base,
                        Cod = w.Cod,
                        Coord = w.Coord,
                        Dt = w.Dt,
                        Clouds = w.Clouds,
                        Main = w.Main,
                        Name = w.Name,
                        Sys = w.Sys,
                        Timezone = w.Timezone,
                        Visibility = w.Visibility,
                        Weather = w.Weather,
                        Wind = w.Wind
                    })
                    .Where(c => c.Name.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
