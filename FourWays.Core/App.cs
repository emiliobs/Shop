namespace FourWays.Core
{
    using FourWays.Core.ViewModels;
    using MvvmCross.IoC;
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            this.RegisterAppStart<TipViewModel>();

            base.Initialize();
        }
    }
}
