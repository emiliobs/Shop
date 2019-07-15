namespace ShopCommon.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);
    }
}
