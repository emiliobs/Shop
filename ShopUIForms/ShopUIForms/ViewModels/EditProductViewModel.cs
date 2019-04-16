namespace ShopUIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using Xamarin.Forms;

    public class EditProductViewModel: BaseViewModels
    {
        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;
        #endregion

        #region Properties
        public Product Product { get; set; }

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

        #region Contructor
        public EditProductViewModel(Product product)
        {
            this.Product = product;  
            this.apiService = new ApiService();
            this.IsEnabled = true;

        }
        #endregion

        #region Commands

        public  ICommand SaveCommand { get => new RelayCommand(Save); }
        public ICommand DeleteCommand { get => new RelayCommand(Delete); }      

        #endregion

        #region Methods

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Product.Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error","You must enter a product name.","Accept");
                return;
            }

            if (this.Product.Price <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error","The price must be a number greather than zero",
                                                                "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var UrlApi = Application.Current.Resources["UrlApi"].ToString();
            var UrlApiProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            var UrlproductsController = Application.Current.Resources["UrlproductsController"].ToString();
            var bearer = Application.Current.Resources["bearer "].ToString();


            var response = await this.apiService.PutAsync(UrlApi,UrlApiProducts,UrlproductsController,Product.Id, 
                                                            Product,bearer,MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled =true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Accept");
                return;
            }

            //aqui leo el regsitro de la rrespuesta del servicio:
            var modificaProduct = (Product)response.Result;
            MainViewModel.GetInstance().Products.UpdateProductInList(modificaProduct);
            await App.Navigator.PopAsync();
        }

        private async void Delete()
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm","Are you sure to delete the product?",
                                                                          "Yes","No");
            if (!confirm)
            {
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;


            var UrlApi = Application.Current.Resources["UrlApi"].ToString();
            var UrlApiProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            var UrlproductsController = Application.Current.Resources["UrlproductsController"].ToString();
            var bearer = Application.Current.Resources["bearer "].ToString();

            var response = await this.apiService.DeleteAsync(UrlApi, UrlApiProducts, UrlproductsController, this.Product.Id,
                                                             bearer,MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message,"Accept");
                return;
            }

            MainViewModel.GetInstance().Products.DeleteProductInList(this.Product.Id);
            await App.Navigator.PopAsync();
        }

        #endregion
    }
}
