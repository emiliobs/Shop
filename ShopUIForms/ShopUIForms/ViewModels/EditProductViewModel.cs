namespace ShopUIForms.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ShopCommon.Models;

    public class EditProductViewModel
    {
        #region Atributtes
        #endregion

        #region Properties
        public Product Product { get; set; }
        #endregion

        #region Contructor
        public EditProductViewModel(Product product)
        {
            this.Product = product;
        } 
        #endregion
    }
}
