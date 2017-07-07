using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class ProductPrice
    {
        public int ProductPriceId { get; set; }
        public int ProductId { get; set; }
        public string Uom { get; set; }
        public decimal? Price { get; set; }
        public int? NameYourOwnPrice { get; set; }
        public decimal? Weight { get; set; }

        public virtual Product Product { get; set; }
    }
}
