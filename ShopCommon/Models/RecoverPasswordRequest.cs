namespace ShopCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

   public  class RecoverPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
