﻿using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class SectionE
    {
        public int EmploymentAppId { get; set; }
        public string SchoolNameOne { get; set; }
        public string SchoolNameTwo { get; set; }
        public string SchoolNameThree { get; set; }
        public string SchoolCityOne { get; set; }
        public string SchoolCityTwo { get; set; }
        public string SchoolCityThree { get; set; }
        public string SchoolStateOne { get; set; }
        public string SchoolStateTwo { get; set; }
        public string SchoolStateThree { get; set; }
        public string StartMonthTwo { get; set; }
        public string StartYearTwo { get; set; }
        public string EndMonthTwo { get; set; }
        public string EndYearTwo { get; set; }
        public string StartMonthThree { get; set; }
        public string StartYearThree { get; set; }
        public string EndMonthThree { get; set; }
        public string EndYearThree { get; set; }
        public string HighSchoolHours { get; set; }
        public string SemesterHoursTwo { get; set; }
        public string SemesterHoursThree { get; set; }
        public string TrimesterHoursTwo { get; set; }
        public string TrimesterHoursThree { get; set; }
        public string QuarterHoursTwo { get; set; }
        public string QuarterHoursThree { get; set; }
        public int? GraduateOne { get; set; }
        public int? GraduateTwo { get; set; }
        public int? GraduateThree { get; set; }
        public int? GraduateGed { get; set; }
        public string GraduateMonthTwo { get; set; }
        public string GraduateMonthThree { get; set; }
        public string GraduateYearTwo { get; set; }
        public string GraduateYearThree { get; set; }
        public string DegreeTwo { get; set; }
        public string DegreeThree { get; set; }
        public string LeftPriorOne { get; set; }
        public string LeftPriorTwo { get; set; }
        public string LeftPriorThree { get; set; }
        public string GpaOne { get; set; }
        public string GpaTwo { get; set; }
        public string GpaThree { get; set; }
        public string GradeOne { get; set; }
        public string GradeTwo { get; set; }
        public string GradeThree { get; set; }
        public int? TranscriptOne { get; set; }
        public int? TranscriptTwo { get; set; }
        public int? TranscriptThree { get; set; }
        public string MajorOne { get; set; }
        public string MajorTwo { get; set; }
        public string MajorThree { get; set; }
        public string MinorOne { get; set; }
        public string MinorTwo { get; set; }
        public string MinorThree { get; set; }
        public string HonorsOne { get; set; }
        public string HonorsTwo { get; set; }
        public string HonorsThree { get; set; }
        public string ActivitiesOne { get; set; }
        public string ActivitiesTwo { get; set; }
        public string ActivitiesThree { get; set; }
        public string AccomplishmentsOne { get; set; }
        public string AccomplishmentsTwo { get; set; }
        public string AccomplishmentsThree { get; set; }
        public string ScholarshipTwo { get; set; }
        public string ScholarshipThree { get; set; }
        public string LoanTwo { get; set; }
        public string LoanThree { get; set; }
        public string WorkTwo { get; set; }
        public string WorkThree { get; set; }
        public string FamilyTwo { get; set; }
        public string FamilyThree { get; set; }
        public string TypeOne { get; set; }
        public string TypeTwo { get; set; }
        public string NameOne { get; set; }
        public string NameTwo { get; set; }
        public string SchoolLocationOne { get; set; }
        public string SchoolLocationTwo { get; set; }
        public string AttendedStartMonthOne { get; set; }
        public string AttendedStartYearOne { get; set; }
        public string AttendedEndMonthOne { get; set; }
        public string AttendedEndYearOne { get; set; }
        public string AttendedStartMonthTwo { get; set; }
        public string AttendedStartYearTwo { get; set; }
        public string AttendedEndMonthTwo { get; set; }
        public string AttendedEndYearTwo { get; set; }
        public string CoursesOne { get; set; }
        public string CoursesTwo { get; set; }
        public int? CompleteOne { get; set; }
        public int? CompleteTwo { get; set; }
        public string CompleteMonthOne { get; set; }
        public string CompleteYearOne { get; set; }
        public string CompleteMonthTwo { get; set; }
        public string CompleteYearTwo { get; set; }
        public string ReferenceNameOne { get; set; }
        public string ReferenceNameTwo { get; set; }
        public string ReferenceNameThree { get; set; }
        public string ReferencePositionOne { get; set; }
        public string ReferencePositionTwo { get; set; }
        public string ReferencePositionThree { get; set; }
        public string ReferenceSchoolOne { get; set; }
        public string ReferenceSchoolTwo { get; set; }
        public string ReferenceSchoolThree { get; set; }
        public string ReferenceTelephoneOne { get; set; }
        public string ReferenceTelephoneTwo { get; set; }
        public string ReferenceTelephoneThree { get; set; }
        public string ReferenceEmailOne { get; set; }
        public string ReferenceEmailTwo { get; set; }
        public string ReferenceEmailThree { get; set; }
        public string LanguageRead { get; set; }
        public string LanguageWrite { get; set; }
        public string LanguageSpeak { get; set; }
        public int? Typing { get; set; }
        public string Speed { get; set; }
        public int? Calculator { get; set; }
        public int? Dictation { get; set; }
        public int? WordProcessing { get; set; }
        public string WordProcessingName { get; set; }
        public int? Spreadsheet { get; set; }
        public string SpreadsheetName { get; set; }
        public string Apprenticeship { get; set; }
        public string ApprenticeshipTime { get; set; }
        public string ApprenticeshipLocation { get; set; }
        public string MachineSkills { get; set; }
        public string OtherTraining { get; set; }
        public int? EngineerTraining { get; set; }
        public string EngineerTrainingState { get; set; }
        public string EngineerTrainingDate { get; set; }
        public int? ProfessionalEngineer { get; set; }
        public string ProfessionalState { get; set; }
        public string ProfessionalLicense { get; set; }
        public string OtherLicenses { get; set; }
        public string Memberships { get; set; }

        public virtual EmploymentApp EmploymentApp { get; set; }
    }
}
