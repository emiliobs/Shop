namespace ShopCommon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;
    public static  class RegexHelper
    {
         public static bool IsValidEmail(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {

                return false;
            }
        }
    }
}
