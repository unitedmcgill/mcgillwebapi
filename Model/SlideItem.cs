using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SlideItem
    {
        public int SlideItemId { get; set; }
        public int SlideId { get; set; }
        public string Description { get; set; }

        public virtual Slide Slide { get; set; }
    }
}
