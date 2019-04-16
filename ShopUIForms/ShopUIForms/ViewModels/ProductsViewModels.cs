namespace ShopUIForms.ViewModels
{
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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

            //aqui me consumo la api:
            var UrlApi = Application.Current.Resources["UrlApi"].ToString();
            var UrlApiProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            var UrlproductsController = Application.Current.Resources["UrlproductsController"].ToString();
            var bearer = Application.Current.Resources["bearer "].ToString();

            Response response = await this.apiService.GetListAsync<Product>(UrlApi, UrlApiProducts, UrlproductsController, bearer,
                                                                            MainViewModel.GetInstance().Token.Token);

            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

                return;
            }


            //aqui casteo por result del api es un object:
            List<Product> myProducts = (List<Product>)response.Result;

            //aqui utilizo el obserbvavlecollectionn
            this.ProductsList = new ObservableCollection<Product>(myProducts.OrderBy(p => p.Name));
        }


        #endregion
    }
}
