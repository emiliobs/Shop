namespace ShopUIForms.ViewModels
{
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class ProductsViewModels : BaseViewModels
    {
        #region Atributtes

        private readonly ApiService apiService;
        private ObservableCollection<ProductItemViewModel> productsList;
        private bool isRefreshing;
        private List<Product> myProductsList;
        #endregion

        #region Properties
        public ObservableCollection<ProductItemViewModel> ProductsList
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
           this.myProductsList = (List<Product>)response.Result;   
            this.RefresProductsList();

            //aqui utilizo el obserbvavlecollectionn
            //this.ProductsList = new ObservableCollection<ProductItemViewModel>(myProducts.OrderBy(p => p.Name));
        }

        public void AddProductToList(Product product)
        {
            this.myProductsList.Add(product);
            this.RefresProductsList();
        }

        public void UpdateProductInList(Product product)
        {
            var previousProduct = this.myProductsList.Where(p => p.Id.Equals(product.Id)).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myProductsList.Remove(previousProduct);
            }

            this.myProductsList.Add(product);
            this.RefresProductsList();
        }

        public void DeleteProductInList(int productId)
        {
            var previousProduct = this.myProductsList.Where(p => p.Id.Equals(productId)).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myProductsList.Remove(previousProduct);
            }

            this.RefresProductsList();
        }

        private void RefresProductsList()
        {
            this.ProductsList = new ObservableCollection<ProductItemViewModel>(this.myProductsList.Select(p => new ProductItemViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                ImageFullPath = p.ImageFullPath,
                IsAvailable = p.IsAvailable,
                LastPurchase = p.LastPurchase,
                LastSale = p.LastSale,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                User = p.User

            }).OrderBy(p => p.Name).ToList());
        }


        #endregion
    }
}
