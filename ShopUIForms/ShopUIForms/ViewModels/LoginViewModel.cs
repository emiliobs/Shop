namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopUIForms.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region Contructors

        public LoginViewModel()
        {
            Email = "emilio@gmail.com";
            Password = "Emilio123.";
        }

        #endregion

        #region Commands

        public ICommand LoginCommand { get => new RelayCommand(Login); }


        #endregion

        #region Methods

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {

                await Application.Current.MainPage.DisplayAlert("Error","You must enter an Email","Accept");

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an Password", "Accept");

                return;
            }


            if (!Email.Equals("barrera_emilio@yahoo.es") || !Password.Equals("Eabs123."))
            {

                await Application.Current.MainPage.DisplayAlert("Error", "User or Password wrong", "Accept");
                return;

            }

            //await Application.Current.MainPage.DisplayAlert("Ok", "Fuck yearrrrr", "Accept"); 


            //Aqui intacion con el singleto del main  viewmodel:
            MainViewModel.GetInstance().Products = new ProductsViewModels();
            //aqui navego a la pagina de products y veo la llista de los mismos:
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());





        }


        #endregion
    }
}
