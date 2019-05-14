namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Helpers;
    using ShopCommon.Services;
    using ShopUIForms.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RememberPasswordViewModel : BaseViewModels
    {
        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiservice;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get => this.isRunning;
            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Email { get; set; }

        #endregion

        #region Constructor;
        public RememberPasswordViewModel()
        {
            this.apiservice = new ApiService();
            this.IsEnabled = true;

        }
        #endregion

        #region Commands
        public ICommand RecoverCommand => new RelayCommand(Recover);

        #endregion

        #region Methods


        private async void Recover()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.EnterEmail ,Languages.Accept);
                return;
            }


            if (!RegexHelper.IsValidEmail(this.Email) )
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ValidEmail, Languages.Accept);
                return;
            }


        }

        #endregion



    }
}
