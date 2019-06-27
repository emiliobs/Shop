namespace ShopUIClassic.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using Newtonsoft.Json;
    using ShopCommon.Models;
    using ShopCommon.Services;
    using ShopUIClassic.Helpers;

    [Activity(Label = "@string/login", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        private ApiService apiService;
        private EditText emailText;
        private EditText passwordText;
        private ProgressBar activityIndicatorProgressBar;
        private Button loginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            this.SetContentView(Resource.Layout.LoginPage);
            this.FindViews();
            this.HandleEvents();
            this.SetInitialData();
        }

        private void SetInitialData()
        {
            this.apiService = new ApiService();
            this.emailText.Text = "jzuluaga55@gmail.com";
            this.passwordText.Text = "123456";
            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
        }

        private void HandleEvents()
        {
            this.loginButton.Click += LoginButton_ClickAsync;
        }

        private async void LoginButton_ClickAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.emailText.Text))
            {
                DiaglogService.ShowMessage(this, "Error","You must enter an email","Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.passwordText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter an password", "Accept");
                return;
            }

            this.activityIndicatorProgressBar.Visibility = ViewStates.Visible;
            this.loginButton.Enabled = false;

            var request = new TokenRequest
            {
               Password = this.passwordText.Text,
               Username = this.emailText.Text,
            };

            var response = await this.apiService.GetTokenAsync("https://shopzulu.azurewebsites.net",
                                                               "/Account","/CreateToken", request);

            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
            this.loginButton.Enabled = true;

            if (!response.IsSuccess)
            {
                DiaglogService.ShowMessage(this,"Error","User or Password incorrect","Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var intent = new Intent(this, typeof(ProductActivity));
            intent.PutExtra("token", JsonConvert.SerializeObject(token));
            intent.PutExtra("email", this.emailText.Text);
            this.StartActivity(intent);


        }

     
        private void FindViews()
        {
            this.emailText = FindViewById<EditText>(Resource.Id.editTextEmail);
            this.passwordText = FindViewById<EditText>(Resource.Id.editTextPassword);
            this.activityIndicatorProgressBar = FindViewById<ProgressBar>(Resource.Id.ActivityIndicatoProgressBar);
            this.loginButton = FindViewById<Button>(Resource.Id.LoginButton);
        }
    }
}