using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SectionF
    {
        public int EmploymentAppId { get; set; }
        public int? Military { get; set; }
        public string MilitaryStartMonth { get; set; }
        public string MilitaryStartYear { get; set; }
        public string MilitaryEndMonth { get; set; }
        public string MilitaryEndYear { get; set; }
        public string Service { get; set; }
        public string Branch { get; set; }
        public string StartRank { get; set; }
        public string EndRank { get; set; }
        public string PrincipleDuties { get; set; }
        public string MilitaryEducation { get; set; }
        public string MilitaryHonors { get; set; }
        public string MilitaryNameOne { get; set; }
        public string MilitaryNameTwo { get; set; }
        public string MilitaryNameThree { get; set; }
        public string MilitaryPositionOne { get; set; }
        public string MilitaryPositionTwo { get; set; }
        public string MilitaryPositionThree { get; set; }
        public string MilitarySchoolOne { get; set; }
        public string MilitarySchoolTwo { get; set; }
        public string MilitarySchoolThree { get; set; }
        public string MilitaryTelephoneOne { get; set; }
        public string MilitaryTelephoneTwo { get; set; }
        public string MilitaryTelephoneThree { get; set; }
        public string MilitaryEmailOne { get; set; }
        public string MilitaryEmailTwo { get; set; }
        public string MilitaryEmailThree { get; set; }

        public virtual EmploymentApp EmploymentApp { get; set; }
    }
}
