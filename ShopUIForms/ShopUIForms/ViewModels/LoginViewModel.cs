namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using ShopCommon.Helpers;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using ShopUIForms.Helpers;
    using ShopUIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel:BaseViewModels
    {

        #region Attributtes

        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private bool isRemember;

        #endregion
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemember
        {
            get => isRemember;
            set
            {
                if (isRemember != value)
                {
                    isRemember = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        #region Contructors

        public LoginViewModel()
        {
            //services
            this.apiService = new ApiService();

            //Boton
            this.IsEnabled = true;
            this.IsRemember = true;

            //Email = "angelina@gmail.com";
            //Password = "123456";
        }

        #endregion

        #region Commands

        public ICommand LoginCommand { get => new RelayCommand(Login); }
        public ICommand RegisterCommand { get => new RelayCommand(Register); }

        #endregion

        #region Methods

        private async void Register()
        {
          MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }



        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error,Languages.EmailError,Languages.Accept);

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PasswordError, Languages.Accept);

                return;
            }


            //if (!Email.Equals("barrera_emilio@yahoo.es") || !Password.Equals("Eabs123."))
            //{

            //    await Application.Current.MainPage.DisplayAlert("Error", "User or Password wrong", "Accept");
            //    return;

            //}

            //await Application.Current.MainPage.DisplayAlert("Ok", "Fuck yearrrrr", "Accept"); 


            this.IsRunning = true;
            this.IsEnabled = false;

            //aqui ya consumo el token de seguridad:
            var request = new TokenRequest()
            {
               Password = this.Password,
               Username = this.Email,
            };

            //aqui me consumo la api:
            var UrlApi = Application.Current.Resources["UrlApi"].ToString();
            var UrlApiAccount = Application.Current.Resources["UrlApiAccount"].ToString();
            var UrlApiCreateToken = Application.Current.Resources["UrlApiCreateToken"].ToString();

            var response = await this.apiService.GetTokenAsync(UrlApi, UrlApiAccount, UrlApiCreateToken, request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error,Languages.LoginError, Languages.Accept);
                return;
            }

            //aqui deserializo el token
            var token = (TokenResponse)response.Result;   
            var mainViewModel = MainViewModel.GetInstance();
            //aqui guardo el password y el usuario en memoria
            mainViewModel.UserEmail = this.Email;
            mainViewModel.UserPassword = this.Password;
            //aqui guaro el toen en memoria:
            mainViewModel.Token = token;
            //Aqui intacion con el singleto del main  viewmodel:
            mainViewModel.Products = new ProductsViewModels();


            //aqui navego a la pagina de products y veo la llista de los mismos:
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());      

            //aqui guaro loa credenciales e persistencia login y password ad token:
            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            //aqui converto el token de object a string
            Settings.Token = JsonConvert.SerializeObject(token);

            //despues del login arranca por el masterdetailpage
            Application.Current.MainPage = new MasterPage();

        }


        #endregion
    }
}
