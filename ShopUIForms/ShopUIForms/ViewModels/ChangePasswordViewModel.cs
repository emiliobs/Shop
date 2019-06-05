﻿namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Helpers;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ChangePasswordViewModel : BaseViewModels
    {
        #region Atributtes
        private readonly ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties

        public String CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirm { get; set; }

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

        #endregion


        #region Contructors

        public ChangePasswordViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        #endregion

        #region Commands
        public ICommand ChangePasswordCommand => new RelayCommand(ChangePassword);

        #endregion

        #region Method

        private async void ChangePassword()
        {
            if (string.IsNullOrEmpty(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the current password.",
                    "Accept");
                return;
            }

            if (!MainViewModel.GetInstance().UserPassword.Equals(this.CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "The current password is incorrect.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.NewPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the new password.",
                    "Accept");
                return;
            }

            if (this.NewPassword.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "The password must have at least 6 characters length.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter the password confirm.",
                    "Accept");
                return;
            }

            if (!this.NewPassword.Equals(this.PasswordConfirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "The password and confirm does not match.",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new ChangePasswordRequest
            {
                Email = MainViewModel.GetInstance().UserEmail,
                NewPassword = this.NewPassword,
                OldPassword = this.CurrentPassword
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            MainViewModel.GetInstance().UserPassword = this.NewPassword;
            Settings.UserPassword = this.NewPassword;

            await Application.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                "Accept");

            await App.Navigator.PopAsync();
        }


        #endregion
    }
}
