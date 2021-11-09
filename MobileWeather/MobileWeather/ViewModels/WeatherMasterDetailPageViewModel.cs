using MobileWeather.Helpers;
using MobileWeather.ItemViewModels;
using MobileWeather.Models;
using MobileWeather.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MobileWeather.ViewModels
{
    public class WeatherMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public WeatherMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public DelegateCommand LogoutCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
                });
            }
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "weatherList",
                    PageName = $"{nameof(WeatherPage)}",
                    Title = Languages.WeatherList
                },

                new Menu
                {
                    Icon = "user",
                    PageName = $"{nameof(ModifyUserPage)}",
                    Title = Languages.ModifyUser
                },

                new Menu
                {
                    Icon = "about",
                    PageName = $"{nameof(AboutPage)}",
                    Title = Languages.About
                },

                //new Menu
                //{
                //    Icon = "history",
                //    PageName = $"{nameof(ShowHistoryPage)}",
                //    Title = "Show history"
                //},



            };


            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}
