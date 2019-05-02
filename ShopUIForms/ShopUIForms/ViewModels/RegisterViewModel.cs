namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using ShopUIForms.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModels
    {

        #region Atribttes
        private bool isRunning;
        private bool isEnabled;
        private ObservableCollection<Country> countries;
        private Country country;
        private ObservableCollection<City> cities;
        private City city;
        private readonly ApiService apiService;  
        #endregion

        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
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
                if (this.isRunning != value)
                {
                    this.isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<Country> Countries
        {
            get=> this.countries;
            set
            {
                if (this.countries != value)
                {
                    this.countries = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Country Country
        {
            get => this.country;
            set
            {
                if (this.country != value)
                {
                    this.country = value;
                    this.Cities = new ObservableCollection<City>(this.Country.Cities.OrderBy(c => c.Name));
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
        #endregion

        #region Contructors
        public RegisterViewModel()
        {
            this.IsEnabled = true;

            this.apiService = new ApiService();

            this.LoadCountries();
        }

       
        #endregion

        #region Commnads
        public ICommand RegisterCommand { get => new RelayCommand(Register); }

        #endregion

        #region Methods

        private async void LoadCountries()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var urlApi = Application.Current.Resources["UrlApi"].ToString();
            var api = Application.Current.Resources["UrlApiProducts"].ToString();
            var CountriesController = Application.Current.Resources["CountriesController"].ToString();
            var response = await this.apiService.GetListAsync<Country>(urlApi, api, CountriesController);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,response.Message,Languages.Accept);

                return;
            }

            //aqui casteo una ana lista de la respuesta que viene del api:
            var myCountries = (List<Country>)response.Result;
            
            this.Countries = new ObservableCollection<Country>(myCountries);
            
        }

        private async void Register()
        {

        }

        #endregion




    }
}
