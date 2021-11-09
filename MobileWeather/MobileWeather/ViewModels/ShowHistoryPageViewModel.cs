using Prism.Mvvm;
using Prism.Navigation;

namespace MobileWeather.ViewModels
{
    public class ShowHistoryPageViewModel : ViewModelBase
    {
        public ShowHistoryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Show weather cities history";
        }
    }
}
