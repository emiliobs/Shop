namespace ShopUIForms.ViewModels
{
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ProductsViewModels : BaseViewModels
    {
        #region Atributtes

        private readonly ApiService apiService;
        private ObservableCollection<Product> productsList;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<Product> ProductsList
        {
            get => productsList;
            set
            {
                if (productsList != value)
                {
                    productsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Cotructor
        public ProductsViewModels()
        {
            //Services:
            apiService = new ApiService();

            LoadProducts();
        }

        #endregion

        #region Methods

        private async void LoadProducts()
        {
            IsRefreshing = true;

            Response response = await this.apiService.GetListAsync<Product>("https://shopzulu.azurewebsites.net", "/api/", "Products");

            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

                return;
            }


            //aqui casteo por result del api es un object:
            List<Product> myProducts = (List<Product>)response.Result;

            //aqui utilizo el obserbvavlecollectionn
            this.ProductsList = new ObservableCollection<Product>(myProducts);
        }


        #endregion
    }
}
