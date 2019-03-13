namespace ShopWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeliverViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Delivery date")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]      
        public DateTime DeliveryDate { get; set; }
    }
}
