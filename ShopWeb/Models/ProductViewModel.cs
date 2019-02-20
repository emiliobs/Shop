namespace ShopWeb.Models
{
    using Microsoft.AspNetCore.Http;
    using ShopWeb.Data.Entities;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
