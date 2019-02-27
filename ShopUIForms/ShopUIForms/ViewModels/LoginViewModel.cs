namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

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

            await Application.Current.MainPage.DisplayAlert("Ok", "Fouck yearrrrr", "Accept");

            

        }


        #endregion
    }
}
