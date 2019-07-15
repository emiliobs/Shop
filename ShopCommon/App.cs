using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ShopCommon.ViewModels;

namespace ShopCommon
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();


            this.RegisterAppStart<LoginViewModel>();
        }
    }                       
}
