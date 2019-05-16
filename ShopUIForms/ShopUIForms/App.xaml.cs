using Newtonsoft.Json;
using ShopCommon.Helpers;
using ShopCommon.Models;
using ShopUIForms.ViewModels;
using ShopUIForms.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShopUIForms
{
    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        #endregion

        public App()
        {
            InitializeComponent();

            //here  aks if the user have your persintence data
            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                //aqui deserializo de un un string a un user class:
                var user = JsonConvert.DeserializeObject<User>(Settings.User);
                if (token.Expiration > DateTime.Now)
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.User = user;
                    mainViewModel.Token = token;
                    mainViewModel.UserEmail = Settings.UserEmail;
                    mainViewModel.UserPassword = Settings.UserPassword;
                    mainViewModel.Products = new ProductsViewModels();
                    this.MainPage = new MasterPage();
                    return;
                }
            }

            //aqui instacion la clase loginviewmodel con el patron singleston del mailviewmodel:
            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
