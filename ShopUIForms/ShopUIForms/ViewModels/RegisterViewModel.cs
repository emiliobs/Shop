namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        }
        #endregion

        #region Commnads
        public ICommand RegisterCommand { get => new RelayCommand(Register); }

        #endregion

        #region Methods

        private async void Register()
        {

        }

        #endregion




    }
}
