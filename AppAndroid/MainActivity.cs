using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AppAndroid.Helper;
using System;
using ShopCommon.Services;
using Android.Views;

namespace AppAndroid
{
    [Activity(Label = "@string/login", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ApiService apiService;
        private EditText emailText;
        private EditText passwordText;
        private ProgressBar activityIndicatorProgressBar;
        private Button loginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
         

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPage);

            emailText = FindViewById<EditText>(Resource.Id.emailText);
            passwordText = FindViewById<EditText>(Resource.Id.passwordText);
            activityIndicatorProgressBar = FindViewById<ProgressBar>(Resource.Id.activityIndicatorProgressBarr);
            loginButton = FindViewById<Button>(Resource.Id.loginButton);

            loginButton.Click += LoginButton_Click;

            this.SetInitialData();                       

        }

        private void SetInitialData()
        {
            apiService = new ApiService();
            this.emailText.Text = "jzuluaga55@gmail.com";
            this.passwordText.Text = "123456";
            //this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(emailText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter an Email.", "Accept");

                return;
            }

            if (string.IsNullOrEmpty(passwordText.Text))
            {
                DiaglogService.ShowMessage(this,"Error","You must enter a Password.","Accept");
                return;
            }

            this.activityIndicatorProgressBar.Visibility = ViewStates.Visible;
            if (!emailText.Text.Equals("emilio@gmail.com") || !passwordText.Text.Equals("55555"))
            {
               DiaglogService.ShowMessage(this, "Next", "User or Password Wrong..", "Accept");
                this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
                return;

            }

            DiaglogService.ShowMessage(this, "Next", "You are inside..", "Accept");


            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}