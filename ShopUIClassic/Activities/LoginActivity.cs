namespace ShopUIClassic.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
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
            //this.emailText.Text = "jzuluaga55@gmail.com";
            //this.passwordText.Text ="123456";
            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
        }

        private void HandleEvents()
        {
            this.loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
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

            DiaglogService.ShowMessage(this, "Perfect", "You are inside.....", "Accept");

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