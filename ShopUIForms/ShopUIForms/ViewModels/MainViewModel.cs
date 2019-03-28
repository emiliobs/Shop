using ShopCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopUIForms.ViewModels
{
    public class MainViewModel
    {

        #region Attributes

        #endregion

        #region Properties
        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }
        public ProductsViewModels Products { get; set; }
        #endregion

        #region Contructor
        public MainViewModel()
        {
            //singleton:
            instance = this;

            //this.Login = new LoginViewModel();
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

        #endregion

        #region Methods

        #endregion

    }
}
