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
        public LoginViewModel Login { get; set; }
        #endregion

        #region Contructor
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Commands

        #endregion

        #region Methods

        #endregion

    }
}
