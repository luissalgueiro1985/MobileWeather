using MobileWeather.Helpers;
using MobileWeather.Services;
using MobileWeather.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileWeather.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly IAPIService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;


        public LoginPageViewModel(INavigationService navigationService,
            IAPIService aPIService) : base(navigationService)
        {
            Title = Languages.Login;
            IsEnabled = true;
            _apiService = aPIService;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));



        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }


        private async void Login()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);

                });
                return;
            }


            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NeedEmail, Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NeedPassword, Languages.Accept);
                return;
            }

            IsRunning = true;

            var isAuth = await _apiService.LoginAsync(Email, Password);

            IsRunning = false;


            if (isAuth)
            {
                var properties = Xamarin.Forms.Application.Current.Properties;

                if (!properties.ContainsKey("username"))
                {
                    properties.Add("username", Email);
                }
                else
                {
                    properties["username"] = Email;
                }

                await NavigationService.NavigateAsync($"/{nameof(WeatherMasterDetailPage)}/NavigationPage/{nameof(WeatherPage)}");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailPasswordIncorrect, Languages.Accept);
            }
        }

    }
}
