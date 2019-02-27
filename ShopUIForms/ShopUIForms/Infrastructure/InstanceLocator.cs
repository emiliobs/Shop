namespace ShopUIForms.Infrastructure
{
    using ShopUIForms.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
