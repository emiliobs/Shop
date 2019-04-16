﻿namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel :BaseViewModels
    {
        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        #endregion

        #region Properties
        public string Image { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Construnctor
        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            this.Image = "noimage";
            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get => new RelayCommand(Save); }


        #endregion

        #region Methods
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a product name.","Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert("Error","You must enter a product price.","Accept");
                return;
            }

            var price = decimal.Parse(this.Price);
            if (price <=  0)
            {
                await Application.Current.MainPage.DisplayAlert("Error","The price must be a number greather tham zero ","Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            //TODO: Add Image


            //aqui envio el objeto al post(
            var product = new Product()
            {
                IsAvailable = true,
                Name = this.Name,
                Price = price,
                User = new User (){ UserName = MainViewModel.GetInstance().UserEmail  }
            };

            var UrlApi = Application.Current.Resources["UrlApi"].ToString();
            var UrlApiProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            var UrlproductsController = Application.Current.Resources["UrlproductsController"].ToString();
            var bearer = Application.Current.Resources["bearer "].ToString();

            var response = await this.apiService.PostAsync(UrlApi,UrlApiProducts,UrlproductsController,
                                                           product,
                                                            bearer,
                                                            MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            //aqui coonvierto lo que vega a product
            var newProduct = (Product)response.Result;
            MainViewModel.GetInstance().Products.AddProductToList(newProduct);

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();

        }
        #endregion
    }
}
