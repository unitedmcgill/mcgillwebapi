using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SectionB
    {
        public int EmploymentAppId { get; set; }
        public int? EmployBefore { get; set; }
        public string BeforeStartMonth { get; set; }
        public string BeforeStartYear { get; set; }
        public string BeforeEndMonth { get; set; }
        public string BeforeEndYear { get; set; }
        public string BeforeJob { get; set; }
        public string BeforeSupervisor { get; set; }
        public string BeforeManager { get; set; }
        public string BeforeLeave { get; set; }
        public int? ApplyBefore { get; set; }
        public string ApplyDate { get; set; }
        public string ApplyJob { get; set; }
        public int? McGillRelatives { get; set; }
        public string RelativeNameOne { get; set; }
        public string RelativeNameTwo { get; set; }
        public string RelativeNameThree { get; set; }
        public string RelativeRelationshipOne { get; set; }
        public string RelativeRelationshipTwo { get; set; }
        public string RelativeRelationshipThree { get; set; }
        public string RelativeJobOne { get; set; }
        public string RelativeJobTwo { get; set; }
        public string RelativeJobThree { get; set; }
        public string RelativeLocationOne { get; set; }
        public string RelativeLocationTwo { get; set; }
        public string RelativeLocationThree { get; set; }
        public string FindOut { get; set; }
        public string FindOutEmployee { get; set; }
        public string FindOutSite { get; set; }
        public string FindOutOther { get; set; }
        public string McGillPosition { get; set; }
        public string McGillWork { get; set; }
        public string McGillWorkHours { get; set; }
        public int? SecondShift { get; set; }
        public int? ThirdShift { get; set; }
        public string StartPayHourly { get; set; }
        public string StartPayYearly { get; set; }
        public string StartWorking { get; set; }
        public int? ContactEmployer { get; set; }
        public string ContactEmployerTime { get; set; }
        public string PlansGoals { get; set; }

        public virtual EmploymentApp EmploymentApp { get; set; }
    }
}
