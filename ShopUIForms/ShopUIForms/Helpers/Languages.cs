namespace ShopUIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;    
        public static string Error => Resource.Error;     
        public static string EmailError => Resource.EmailError;
        public static string PasswordError => Resource.PasswordError;
        public static string LoginError => Resource.LoginError;
        public static string Login => Resource.Login;
        public static string Password => Resource.Password;
        public static string Rememberme => Resource.Rememberme;
        public static string EnterEmail => Resource.EnterEmail;
        public static string EnterPassword => Resource.EnterPassword;
        public static string Email => Resource.Email;
        public static string WhereTakePicture => Resource.WhereTakePicture;
        public static string Cancel => Resource.Cancel;
        public static string FromCamera => Resource.FromCamera;
        public static string FromGallery => Resource.FromGallery;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string FirstName => Resource.FirstName;
        public static string LatsName => Resource.LatsName;
        public static string ValidEmail => Resource.ValidEmail;
        public static string SelectCity => Resource.SelectCity;
        public static string SelectCountry => Resource.SelectCountry;
        public static string EnterPhone => Resource.EnterPhone;
        public static string LogitudPassword => Resource.LogitudPassword;
        public static string ConfirmPassword => Resource.ConfirmPassword;
        public static string OK => Resource.OK;
        public static string ForgotPassword => Resource.ForgotPassword;
    }

}
