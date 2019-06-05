namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using ShopCommon.Helpers;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using ShopUIForms.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProfileViewModel : BaseViewModels
    {
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private User user;
        private List<Country> myCountries;

        public Country Country
        {
            get => this.country;
            set
            {
                if (this.country != value)
                {
                    this.country = value;
                    NotifyPropertyChanged();
                }
                this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
            }
        }

        public City City
        {
            get => this.city;
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public User User
        {
            get => this.user;
            set
            {
                if (this.user != value)
                {
                    this.user = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Country> Countries
        {
            get => this.countries;
            set
            {
                if (this.countries != value)
                {
                    this.countries = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<City> Cities
        {
            get => this.cities;
            set
            {
                if (this.cities != value)
                {
                    this.cities = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsRunning
        {
            get => this.isRunning;
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ProfileViewModel()
        {
            this.apiService = new ApiService();
            this.User = MainViewModel.GetInstance().User;
            this.IsEnabled = true;
            this.LoadCountries();
        }

        #region Commands

        public ICommand SaveCommand => new RelayCommand(this.Save);
        public ICommand ModifyPasswordCommand => new RelayCommand(ModifyPassword);

        #endregion

        #region Methods

        private async void ModifyPassword()
        {
            MainViewModel.GetInstance().ChangePassword = new ChangePasswordViewModel();
            await App.Navigator.PushAsync(new ChangePasswordPage());
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the first name.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the last name.",
                    "Accept");
                return;
            }

            if (this.Country == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a country.",
                    "Accept");
                return;
            }

            if (this.City == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must select a city.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.User.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an address.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.User.PhoneNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a phone number.",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PutAsync(
                url,
                "/api",
                "/Account",
                this.User,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            MainViewModel.GetInstance().User = this.User;
            Settings.User = JsonConvert.SerializeObject(this.User);

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                "User updated!",
                "Accept");
            await App.Navigator.PopAsync();


        }

        private async void LoadCountries()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            //var url = Application.Current.Resources["UrlApi"].ToString();
            //var response = await this.apiService.GetListAsync<Country>(
            //    url,
            //    "/api",
            //    "/Countries");

            var response = await this.apiService.GetListAsync<Country>(
               "https://shopzulu.azurewebsites.net",
                "/api",
                "/Countries");

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.myCountries = (List<Country>)response.Result;
            this.Countries = new ObservableCollection<Country>(myCountries);
            this.SetCountryAndCity();
        }

        private void SetCountryAndCity()
        {
            foreach (var country in this.myCountries)
            {
                var city = country.Cities.Where(c => c.Id == this.User.CityId).FirstOrDefault();
                if (city != null)
                {
                    this.Country = country;
                    this.City = city;
                    return;
                }
            }

        } 
        #endregion
    }
}
