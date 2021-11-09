using MobileWeather.Helpers;
using MobileWeather.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace MobileWeather.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAPIService _aPIService;

        public ModifyUserPageViewModel(INavigationService navigationService,IAPIService aPIService) : base(navigationService)
        {
            _navigationService = navigationService;
            _aPIService = aPIService;
            Title = Languages.ModifyUser;
            Task.Run(() => this.GetUser()).Wait();
        }

        public DelegateCommand ModifyUserCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var properties = Xamarin.Forms.Application.Current.Properties;

                    if (properties.ContainsKey("username"))
                    {
                        var savedUsername = (string)properties["username"];
                    }

                    var userTask = await _aPIService.UserChange(FirstName, LastName, Address, FiscalNumber, CitizenCardNumber, Age);

                    if (userTask != null)
                    {
                        await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ChangeOk, Languages.Accept);

                        FirstName = userTask.FirstName;
                        LastName = userTask.LastName;
                        Address = userTask.Address;
                        FiscalNumber = userTask.FiscalNumber;
                        CitizenCardNumber = userTask.CitizenCardNumber;
                        Age = userTask.Age;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RetryLater, Languages.Accept);
                    }
                });
            }
        }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int FiscalNumber { get; set; }

        public int CitizenCardNumber { get; set; }

        public int Age { get; set; }

        private async Task GetUser()
        {
            try
            {
                string savedUsername = string.Empty;
                var properties = Xamarin.Forms.Application.Current.Properties;

                if (properties.ContainsKey("username"))
                {
                    savedUsername = (string)properties["username"];
                }

                var userTask = await _aPIService.GetUserAsync(savedUsername);

                FirstName = userTask.FirstName;
                LastName = userTask.LastName;
                Address = userTask.Address;
                FiscalNumber = userTask.FiscalNumber;
                CitizenCardNumber = userTask.CitizenCardNumber;
                Age = userTask.Age;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RetryLater, Languages.Accept);
            }
        }
    }
}
