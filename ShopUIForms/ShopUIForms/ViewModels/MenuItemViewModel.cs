namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Helpers;
    using ShopUIForms.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel : ShopCommon.Models.Menu
    {
        #region Commands
        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);
        #endregion

        #region Methods
        private async void SelectMenu()
        {

            App.Master.IsPresented = false;

            var mainViewModel = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "ProfilePage":
                    mainViewModel.Profile = new ProfileViewModel();
                    await App.Navigator.PushAsync(new ProfilePage());
                    break;
                default:

                    //cuando el usuaario haga logout, limpio el setting:
                    Settings.User = string.Empty;

                    //here clear of  persitence datas
                    Settings.IsRemember = false;
                    Settings.Token = string.Empty;
                    Settings.UserEmail = string.Empty;
                    Settings.UserPassword = string.Empty;

                    mainViewModel.Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }

        #endregion
    }
}
