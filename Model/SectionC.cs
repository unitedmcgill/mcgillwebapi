using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SectionC
    {
        public int EmploymentAppId { get; set; }
        public string FullName { get; set; }
        public string OtherName { get; set; }
        public string Ssn { get; set; }
        public int? LawfullyEmployable { get; set; }
        public int? OverEighteen { get; set; }
        public int? WorkPermit { get; set; }
        public string PresentMiles { get; set; }
        public string PresentStreet { get; set; }
        public string PresentCity { get; set; }
        public string PresentState { get; set; }
        public string PresentZip { get; set; }
        public string PresentTelephone { get; set; }
        public string PresentEmail { get; set; }
        public string PresentStartMonth { get; set; }
        public string PresentStartYear { get; set; }
        public string PermanentStreet { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentState { get; set; }
        public string PermanentZip { get; set; }
        public string PermanentTelephone { get; set; }
        public string PermanentEmail { get; set; }
        public string PreviousStreetOne { get; set; }
        public string PreviousCityOne { get; set; }
        public string PreviousStateOne { get; set; }
        public string PreviousZipOne { get; set; }
        public string PreviousTelephoneOne { get; set; }
        public string PreviousEmailOne { get; set; }
        public string PreviousStreetTwo { get; set; }
        public string PreviousCityTwo { get; set; }
        public string PreviousStateTwo { get; set; }
        public string PreviousZipTwo { get; set; }
        public string PreviousTelephoneTwo { get; set; }
        public string PreviousEmailTwo { get; set; }
        public int? PerformFunctions { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyRelationship { get; set; }
        public string EmergencyTelephone { get; set; }
        public string EmergencyAddress { get; set; }
        public int? Travel { get; set; }
        public string TravelPercentage { get; set; }
        public string TravelComment { get; set; }
        public int? Relocate { get; set; }
        public string RelocateRestrictions { get; set; }
        public string Transportation { get; set; }
        public string LicenseStateOne { get; set; }
        public string LicenseNumberOne { get; set; }
        public string LicenseStateTwo { get; set; }
        public string LicenseNumberTwo { get; set; }
        public string LicenseStateThree { get; set; }
        public string LicenseNumberThree { get; set; }
        public string LicenseStateFour { get; set; }
        public string LicenseNumberFour { get; set; }
        public int? LicenseSuspend { get; set; }
        public string LicenseWhen { get; set; }
        public string LicenseExplain { get; set; }
        public int? Insurance { get; set; }
        public int? Crime { get; set; }
        public string CrimeExplain { get; set; }
        public string LicenseExpirationOne { get; set; }
        public string LicenseExpirationTwo { get; set; }
        public string LicenseExpirationThree { get; set; }

        public virtual EmploymentApp EmploymentApp { get; set; }
    }
}
