namespace ShopUIClassic.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using Newtonsoft.Json;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using ShopUIClassic.Adapter;
    using ShopUIClassic.Helpers;

    [Activity(Label = "@string/products", Theme = "@style/AppTheme")]
    public class ProductActivity : AppCompatActivity
    {
        #region Atributtes

        private TokenResponse token;
        private string email;
        private ApiService apiservice;
        private ListView productListView;

        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            this.SetContentView(Resource.Layout.ProductsPage);

            this.productListView = this.FindViewById<ListView>(Resource.Id.productsListView);

            this.email = Intent.Extras.GetString("email");
            var tokenStrig = Intent.Extras.GetString("token");
            this.token = JsonConvert.DeserializeObject<TokenResponse>(tokenStrig);

            this.apiservice = new ApiService();
            this.LoadProducts();

        }

        #region Methods
        private async void LoadProducts()
        {
            var response = await this.apiservice.GetListAsync<Product>("https://shopzulu.azurewebsites.net", 
                                                                       "/api","/products","bearer",this.token.Token);

            if (!response.IsSuccess)
            {
                DiaglogService.ShowMessage(this,"Error",response.Message, "Accept");
                return;
            }

            var products = (List<Product>)response.Result;
            this.productListView.Adapter = new ProductsListAdapter(this, products);
            this.productListView.FastScrollEnabled = true;
        } 
        #endregion
    }
}