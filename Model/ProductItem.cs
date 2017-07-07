using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class ProductItem
    {
        public int ProductItemId { get; set; }
        public int ProductId { get; set; }
        public string ItemLine { get; set; }

        public virtual Product Product { get; set; }
    }
}
