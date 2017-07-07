using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class Slide
    {
        public Slide()
        {
            SlideItem = new HashSet<SlideItem>();
        }

        public int SlideId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public virtual ICollection<SlideItem> SlideItem { get; set; }
    }
}
