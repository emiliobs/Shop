using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using FourWays.Core.Services;
using System;

namespace FourWays.Classic.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Attributes

        private EditText amountEditText;
        private SeekBar generositySeekBar;
        private TextView tipTextView;
        private ICalculationService calculationService;


        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            this.calculationService = new CalculationService();
            this.FindViews();
            this.SetupEvents();

            this.RefreshTip();

        }

        private void SetupEvents()
        {
            this.amountEditText.TextChanged += AmountEditText_TextChanged;
            this.generositySeekBar.ProgressChanged += GenerositySeekBar_ProgressChanged;
        }

        private void GenerositySeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            this.RefreshTip();
        }

        private void AmountEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            this.RefreshTip();
        }

        private void RefreshTip()
        {
            var amount = Convert.ToDecimal(this.amountEditText.Text);
            var generosity = (double)this.generositySeekBar.Progress;
            this.tipTextView.Text = $"{this.calculationService.TipAmount(amount, generosity):C2}";
        }

        private void FindViews()
        {
            this.amountEditText = this.FindViewById<EditText>(Resource.Id.amountEditText);
            this.generositySeekBar = this.FindViewById<SeekBar>(Resource.Id.generositySeekBar);
            this.tipTextView = this.FindViewById<TextView>(Resource.Id.tipTextView);
        }





        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}