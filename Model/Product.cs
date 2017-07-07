using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class Product
    {
        public Product()
        {
            ProductItem = new HashSet<ProductItem>();
            ProductPrice = new HashSet<ProductPrice>();
        }

        public int ProductId { get; set; }
        public string Code { get; set; }
        public string SubCode { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string SafetyHref { get; set; }
        public string SafetyDesc { get; set; }
        public int? SortField { get; set; }
        public int? LeedCompliant { get; set; }

        public virtual ICollection<ProductItem> ProductItem { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    }
}
