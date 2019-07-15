namespace ShopCommon.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.ViewModels;
    using ShopCommon.Interfaces;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class LoginViewModel : MvxViewModel
    {
        private string email;
        private string password;
        private MvxCommand loginCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set => this.SetProperty(ref isLoading, value);
        }

        public string Email { get => this.email; set => this.SetProperty(ref this.email, value); }

        public string Password { get => this.password; set => this.SetProperty(ref this.password, value); }

        public ICommand LoginCommad
        {
            get
            {
                //if (this.loginCommand.Equals(null))
                //{
                //    this.loginCommand = new MvxCommand(this.DoLoginCommand);
                //}   
                this.loginCommand = this.loginCommand ?? new MvxCommand(this.DoLoginCommandAsync);
                return loginCommand;
            }
        }

        public LoginViewModel(IApiService apiService, IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;


            this.Email = "jzuluaga55@gmail.com";
            this.Password = "123456";
            this.IsLoading = false;
        }

        private async  void DoLoginCommandAsync()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an Email.","Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a Password.", "Accept");
                return;
            }

            this.IsLoading = true;

            var request = new TokenRequest
            {
                 Password = this.Password,
                 Username = this.Email,
            };


            var response = await this.apiService.GetTokenAsync("","/ACCount","/CreateToken",request);

            if (!response.IsSuccess)
            {
                this.IsLoading = false;
                this.dialogService.Alert("Error", "User or Password incorrect","Accept");
                return;
            }

            this.IsLoading = false;

            this.dialogService.Alert("OK","Fuck Yeah!!","Accept");

        } 
    }
}
