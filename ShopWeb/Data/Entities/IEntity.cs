namespace ShopWeb.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IEntity
    {
          int Id { get; set; }
         //bool WasDeleted { get; set; }
    }
}
