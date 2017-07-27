using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SectionA
    {
        public int EmploymentAppId { get; set; }
        public string Locations { get; set; }
        public string OtherLocation { get; set; }
        public string AgreeText { get; set; }
        public int? AgreeTime { get; set; }

        public virtual EmploymentApp EmploymentApp { get; set; }
    }
}
