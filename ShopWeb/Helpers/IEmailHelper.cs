namespace ShopWeb.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IEmailHelper
    {
        void sendMail(string to, string subject, string body);
    }
}
