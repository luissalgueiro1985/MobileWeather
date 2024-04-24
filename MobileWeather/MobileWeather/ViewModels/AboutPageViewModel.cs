using MobileWeather.Helpers;
using Prism.Navigation;

namespace MobileWeather.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;


        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.About;
            ShowInfo();
        }

        private void ShowInfo()
        {
            Author = "Luís Salgueiro";
            Date = "4-11-2021";
            Version = "1.0.0";
        }


        public string Author { get; set; }

        public string Date { get; set; }

        public string Version { get; set; }
    }
}
