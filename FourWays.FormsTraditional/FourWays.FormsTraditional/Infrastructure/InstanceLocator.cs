namespace FourWays.FormsTraditional.Infrastructure
{
    using FourWays.FormsTraditional.ViewModels;
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
