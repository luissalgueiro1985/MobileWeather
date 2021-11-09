using MobileWeather.Interfaces;
using MobileWeather.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWeather.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Temperature => Resource.Temperature;

        public static string WeatherStatus => Resource.WeatherStatus;


        public static string Login => Resource.Login;

        public static string Register => Resource.Register;

        public static string ModifyUser => Resource.ModifyUser;

        public static string FirstName => Resource.FirstName;

        public static string LastName => Resource.LastName;

        public static string Password => Resource.Password;

        public static string Email => Resource.Email;

        public static string FiscalNumber => Resource.FiscalNumber;

        public static string CitizenCardNumber => Resource.CitizenCardNumber;

        public static string Age => Resource.Age;

        public static string Logout => Resource.Logout;

        public static string About => Resource.About;

        public static string Weather => Resource.Weather;

        public static string City => Resource.City;

        public static string CurrentWeather => Resource.CurrentWeather;

        public static string Search => Resource.Search;

        public static string SearchCity => Resource.SearchCity;

        public static string NeedEmail => Resource.NeedEmail;

        public static string NeedPassword => Resource.NeedPassword;

        public static string EnterEmail => Resource.EnterEmail;

        public static string EnterPassword => Resource.EnterPassword;

        public static string EmailPasswordIncorrect=> Resource.PasswordEmailIncorrect;

        public static string Author => Resource.Author;

        public static string Version => Resource.Version;

        public static string Date => Resource.Date;

        public static string Name => Resource.Name;

        public static string Address => Resource.Address;

        public static string Timezone => Resource.Timezone;

        public static string Cloudiness => Resource.Cloudiness;

        public static string Description => Resource.Description;

        public static string Humidity => Resource.Humidity;

        public static string WindSpeed => Resource.WindSpeed;

        public static string WindDirection => Resource.WindDirection;

        public static string ChangeOk => Resource.ChangeOk;

        public static string RetryLater => Resource.RetryLater;

        public static string WeatherList => Resource.WeatherList;

        public static string Language => Resource.Language;

        public static string Longitude => Resource.Longitude;

        public static string Latitude => Resource.Latitude;


        public static string Confirm => Resource.Confirm;


    }
}
