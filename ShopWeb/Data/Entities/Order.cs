namespace ShopWeb.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    public class Order : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Delivery date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        [Required]
        public User User { get; set; }

        public IEnumerable<OrderDetail> Items { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines { get => Items == null ? 0 : Items.Count(); }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity); } }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Value); } }

        [Required]
        [Display(Name = "Order date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDateLocal
        {
           get
            {
                if (OrderDate == null)
                {
                    return null;
                }

                return OrderDate.ToLocalTime();
            }
        }

        //[Required]
        //[Display(Name = "Delivery date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        //public DateTime? DeliveryDateDateLocal
        //{
        //    get
        //    {
        //        if (DeliveryDate == null)
        //        {
        //            return null;
        //        }

        //        return DeliveryDate.ToLocalTime();
        //    }
        //}

    }

}
