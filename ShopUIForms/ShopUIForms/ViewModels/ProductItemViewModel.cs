namespace ShopUIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using ShopCommon.Models;
    using ShopUIForms.Views;
    using System.Windows.Input;

    public class ProductItemViewModel : Product
    {
        public ICommand SelectProductCommand => new RelayCommand(this.SelectProduct);

        private async void SelectProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel((Product)this);
            await App.Navigator.PushAsync(new EditProductPage());
        }

    }
}
