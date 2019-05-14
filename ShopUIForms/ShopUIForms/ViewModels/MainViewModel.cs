using GalaSoft.MvvmLight.Command;
using ShopCommon.Models;
using ShopUIForms.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ShopUIForms.ViewModels
{
    public class MainViewModel
    {

        #region Attributes

        #endregion

        #region Properties

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }
        public ProductsViewModels Products { get; set; }
        public AddProductViewModel  AddProduct { get; set; }
        public EditProductViewModel EditProduct  { get; set; }
        public RegisterViewModel  Register { get; set; }
        public RememberPasswordViewModel RememberPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        #endregion

        #region Contructor
        public MainViewModel()
        {
            //singleton:
            instance = this;

            //this.Login = new LoginViewModel();

            this.loadMenus();
        }


        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands

        public ICommand AddProductCommand { get => new RelayCommand(this.GoAddProduct); }

        
        #endregion

        #region Methods
        private void loadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_info",
                    PageName = "AboutPage",
                    Title = "About"
                },

                new Menu
                {
                    Icon = "ic_phonelink_setup",
                    PageName = "SetupPage",
                    Title = "Setup"
                },

                new Menu
                {
                    Icon = "ic_exit_to_app.png",
                    PageName = "LoginPage",
                    Title = "Close Session"
                }


            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title,
              }).ToList());
        }

        private async void GoAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await App.Navigator.PushAsync(new  AddProductPage());
        }


        #endregion

    }
}
