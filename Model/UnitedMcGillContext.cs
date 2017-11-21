using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace McGillWebAPI.Model
{
    public partial class UnitedMcGillContext : DbContext
    {
        public virtual DbSet<EmploymentApp> EmploymentApp { get; set; }
        public virtual DbSet<SectionA> SectionA { get; set; }
        public virtual DbSet<SectionB> SectionB { get; set; }
        public virtual DbSet<SectionC> SectionC { get; set; }
        public virtual DbSet<SectionD> SectionD { get; set; }
        public virtual DbSet<SectionE> SectionE { get; set; }
        public virtual DbSet<SectionF> SectionF { get; set; }
        public virtual DbSet<Slide> Slide { get; set; }
        public virtual DbSet<SlideItem> SlideItem { get; set; }

        // Unable to generate entity type for table 'dbo.User'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning You need to set the environment variable SPIDERMAN_PASSWORD
            var password = Environment.GetEnvironmentVariable("SPIDERMAN_PASSWORD");
            optionsBuilder.UseSqlServer(@"Server=SPIDERMAN\mcgillweb;Database=UnitedMcGill;User ID=sa;Password="+password+";");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmploymentApp>(entity =>
            {
                entity.Property(e => e.Code).HasColumnType("varchar(10)");

                entity.Property(e => e.Creator).HasColumnType("varchar(30)");

                entity.Property(e => e.Email).HasColumnType("varchar(75)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(30)");

                entity.Property(e => e.LastName).HasColumnType("varchar(30)");

                entity.Property(e => e.Status).HasColumnType("char(1)");
            });

            modelBuilder.Entity<SectionA>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__SectionA__8D6582C607020F21");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.AgreeText).HasColumnType("varchar(60)");

                entity.Property(e => e.Locations)
                    .HasColumnType("char(9)")
                    .HasDefaultValueSql("'000000000'");

                entity.Property(e => e.OtherLocation).HasColumnType("text");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionA)
                    .HasForeignKey<SectionA>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionA_EmploymentApp");
            });

            modelBuilder.Entity<SectionB>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__SectionB__8D6582C60BC6C43E");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.ApplyBefore).HasDefaultValueSql("'-1'");

                entity.Property(e => e.ApplyDate).HasColumnType("varchar(30)");

                entity.Property(e => e.ApplyJob).HasColumnType("varchar(85)");

                entity.Property(e => e.BeforeEndMonth).HasColumnType("varchar(3)");

                entity.Property(e => e.BeforeEndYear).HasColumnType("varchar(4)");

                entity.Property(e => e.BeforeJob).HasColumnType("varchar(80)");

                entity.Property(e => e.BeforeLeave).HasColumnType("text");

                entity.Property(e => e.BeforeManager).HasColumnType("varchar(30)");

                entity.Property(e => e.BeforeStartMonth).HasColumnType("varchar(3)");

                entity.Property(e => e.BeforeStartYear).HasColumnType("varchar(4)");

                entity.Property(e => e.BeforeSupervisor).HasColumnType("varchar(25)");

                entity.Property(e => e.ContactEmployer).HasDefaultValueSql("'-1'");

                entity.Property(e => e.ContactEmployerTime).HasColumnType("varchar(40)");

                entity.Property(e => e.EmployBefore).HasDefaultValueSql("'-1'");

                entity.Property(e => e.FindOut)
                    .HasColumnType("char(8)")
                    .HasDefaultValueSql("'00000000'");

                entity.Property(e => e.FindOutEmployee).HasColumnType("varchar(25)");

                entity.Property(e => e.FindOutOther).HasColumnType("varchar(50)");

                entity.Property(e => e.FindOutSite).HasColumnType("varchar(43)");

                entity.Property(e => e.McGillPosition).HasColumnType("varchar(75)");

                entity.Property(e => e.McGillRelatives).HasDefaultValueSql("'-1'");

                entity.Property(e => e.McGillWork)
                    .HasColumnType("char(3)")
                    .HasDefaultValueSql("'000'");

                entity.Property(e => e.McGillWorkHours)
                    .HasColumnName("McGillWork_hours")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.PlansGoals).HasColumnType("text");

                entity.Property(e => e.RelativeJobOne)
                    .HasColumnName("RelativeJob_one")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RelativeJobThree)
                    .HasColumnName("RelativeJob_three")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RelativeJobTwo)
                    .HasColumnName("RelativeJob_two")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RelativeLocationOne)
                    .HasColumnName("RelativeLocation_one")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.RelativeLocationThree)
                    .HasColumnName("RelativeLocation_three")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.RelativeLocationTwo)
                    .HasColumnName("RelativeLocation_two")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.RelativeNameOne).HasColumnType("varchar(30)");

                entity.Property(e => e.RelativeNameThree).HasColumnType("varchar(30)");

                entity.Property(e => e.RelativeNameTwo).HasColumnType("varchar(30)");

                entity.Property(e => e.RelativeRelationshipOne).HasColumnType("varchar(20)");

                entity.Property(e => e.RelativeRelationshipThree).HasColumnType("varchar(20)");

                entity.Property(e => e.RelativeRelationshipTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.SecondShift).HasDefaultValueSql("'-1'");

                entity.Property(e => e.StartPayHourly).HasColumnType("varchar(6)");

                entity.Property(e => e.StartPayYearly).HasColumnType("varchar(6)");

                entity.Property(e => e.StartWorking).HasColumnType("varchar(55)");

                entity.Property(e => e.ThirdShift).HasDefaultValueSql("'-1'");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionB)
                    .HasForeignKey<SectionB>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionB_EmploymentApp");
            });

            modelBuilder.Entity<SectionC>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__SectionC__8D6582C6173876EA");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.Crime).HasDefaultValueSql("'-1'");

                entity.Property(e => e.CrimeExplain).HasColumnType("text");

                entity.Property(e => e.EmergencyAddress).HasColumnType("varchar(90)");

                entity.Property(e => e.EmergencyName).HasColumnType("varchar(50)");

                entity.Property(e => e.EmergencyRelationship).HasColumnType("varchar(15)");

                entity.Property(e => e.EmergencyTelephone).HasColumnType("varchar(12)");

                entity.Property(e => e.FullName).HasColumnType("varchar(75)");

                entity.Property(e => e.Insurance).HasDefaultValueSql("'-1'");

                entity.Property(e => e.LawfullyEmployable).HasDefaultValueSql("'-1'");

                entity.Property(e => e.LicenseExpirationOne).HasColumnType("varchar(10)");

                entity.Property(e => e.LicenseExpirationThree).HasColumnType("varchar(10)");

                entity.Property(e => e.LicenseExpirationTwo).HasColumnType("varchar(10)");

                entity.Property(e => e.LicenseExplain).HasColumnType("varchar(75)");

                entity.Property(e => e.LicenseNumberFour).HasColumnType("varchar(12)");

                entity.Property(e => e.LicenseNumberOne).HasColumnType("varchar(12)");

                entity.Property(e => e.LicenseNumberThree).HasColumnType("varchar(12)");

                entity.Property(e => e.LicenseNumberTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.LicenseStateFour).HasColumnType("varchar(2)");

                entity.Property(e => e.LicenseStateOne).HasColumnType("varchar(2)");

                entity.Property(e => e.LicenseStateThree).HasColumnType("varchar(2)");

                entity.Property(e => e.LicenseStateTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.LicenseSuspend).HasDefaultValueSql("'-1'");

                entity.Property(e => e.LicenseWhen).HasColumnType("varchar(30)");

                entity.Property(e => e.OtherName).HasColumnType("varchar(55)");

                entity.Property(e => e.OverEighteen).HasDefaultValueSql("'-1'");

                entity.Property(e => e.PerformFunctions).HasDefaultValueSql("'-1'");

                entity.Property(e => e.PermanentCity).HasColumnType("varchar(20)");

                entity.Property(e => e.PermanentEmail).HasColumnType("varchar(30)");

                entity.Property(e => e.PermanentState).HasColumnType("varchar(2)");

                entity.Property(e => e.PermanentStreet).HasColumnType("varchar(30)");

                entity.Property(e => e.PermanentTelephone).HasColumnType("varchar(12)");

                entity.Property(e => e.PermanentZip).HasColumnType("varchar(5)");

                entity.Property(e => e.PresentCity).HasColumnType("varchar(20)");

                entity.Property(e => e.PresentEmail).HasColumnType("varchar(30)");

                entity.Property(e => e.PresentMiles).HasColumnType("varchar(4)");

                entity.Property(e => e.PresentStartMonth)
                    .HasColumnName("PresentStart_month")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.PresentStartYear).HasColumnType("varchar(4)");

                entity.Property(e => e.PresentState).HasColumnType("varchar(2)");

                entity.Property(e => e.PresentStreet).HasColumnType("varchar(30)");

                entity.Property(e => e.PresentTelephone).HasColumnType("varchar(12)");

                entity.Property(e => e.PresentZip).HasColumnType("varchar(5)");

                entity.Property(e => e.PreviousCityOne).HasColumnType("varchar(20)");

                entity.Property(e => e.PreviousCityTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.PreviousEmailOne).HasColumnType("varchar(30)");

                entity.Property(e => e.PreviousEmailTwo).HasColumnType("varchar(30)");

                entity.Property(e => e.PreviousStateOne).HasColumnType("varchar(2)");

                entity.Property(e => e.PreviousStateTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.PreviousStreetOne).HasColumnType("varchar(30)");

                entity.Property(e => e.PreviousStreetTwo).HasColumnType("varchar(30)");

                entity.Property(e => e.PreviousTelephoneOne).HasColumnType("varchar(12)");

                entity.Property(e => e.PreviousTelephoneTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.PreviousZipOne).HasColumnType("varchar(5)");

                entity.Property(e => e.PreviousZipTwo).HasColumnType("varchar(5)");

                entity.Property(e => e.Relocate).HasDefaultValueSql("'-1'");

                entity.Property(e => e.RelocateRestrictions).HasColumnType("varchar(60)");

                entity.Property(e => e.Ssn)
                    .HasColumnName("SSN")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.Transportation).HasColumnType("varchar(60)");

                entity.Property(e => e.Travel).HasDefaultValueSql("'-1'");

                entity.Property(e => e.TravelComment).HasColumnType("varchar(80)");

                entity.Property(e => e.TravelPercentage).HasColumnType("varchar(4)");

                entity.Property(e => e.WorkPermit).HasDefaultValueSql("'-1'");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionC)
                    .HasForeignKey<SectionC>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionC_EmploymentApp");
            });

            modelBuilder.Entity<SectionD>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__SectionD__8D6582C6239E4DCF");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.AdditionExplainFirst).HasColumnType("varchar(40)");

                entity.Property(e => e.AdditionExplainSecond).HasColumnType("varchar(40)");

                entity.Property(e => e.AdditionExplainThird).HasColumnType("varchar(40)");

                entity.Property(e => e.AdditionUnitFirst).HasColumnType("varchar(5)");

                entity.Property(e => e.AdditionUnitSecond).HasColumnType("varchar(5)");

                entity.Property(e => e.AdditionUnitThird).HasColumnType("varchar(5)");

                entity.Property(e => e.AdditionWageFirst).HasColumnType("varchar(6)");

                entity.Property(e => e.AdditionWageSecond).HasColumnType("varchar(6)");

                entity.Property(e => e.AdditionWageThird).HasColumnType("varchar(6)");

                entity.Property(e => e.AddressFirst).HasColumnType("varchar(85)");

                entity.Property(e => e.AddressSecond).HasColumnType("varchar(85)");

                entity.Property(e => e.AddressThird).HasColumnType("varchar(85)");

                entity.Property(e => e.DepartmentFirst).HasColumnType("text");

                entity.Property(e => e.DepartmentSecond).HasColumnType("text");

                entity.Property(e => e.DepartmentThird).HasColumnType("text");

                entity.Property(e => e.DescriptionFirst).HasColumnType("text");

                entity.Property(e => e.DescriptionSecond).HasColumnType("text");

                entity.Property(e => e.DescriptionThird).HasColumnType("text");

                entity.Property(e => e.DutiesFirst).HasColumnType("text");

                entity.Property(e => e.DutiesSecond).HasColumnType("text");

                entity.Property(e => e.DutiesThird).HasColumnType("text");

                entity.Property(e => e.EmployerFirst).HasColumnType("varchar(60)");

                entity.Property(e => e.EmployerSecond).HasColumnType("varchar(60)");

                entity.Property(e => e.EmployerThird).HasColumnType("varchar(60)");

                entity.Property(e => e.EndMonthFirst).HasColumnType("varchar(3)");

                entity.Property(e => e.EndMonthSecond).HasColumnType("varchar(3)");

                entity.Property(e => e.EndMonthThird).HasColumnType("varchar(3)");

                entity.Property(e => e.EndYearFirst).HasColumnType("varchar(4)");

                entity.Property(e => e.EndYearSecond).HasColumnType("varchar(4)");

                entity.Property(e => e.EndYearThird).HasColumnType("varchar(4)");

                entity.Property(e => e.HealthCareFirst).HasDefaultValueSql("'-1'");

                entity.Property(e => e.HealthCareSecond).HasDefaultValueSql("'-1'");

                entity.Property(e => e.HealthCareThird).HasDefaultValueSql("'-1'");

                entity.Property(e => e.LeavingFirst).HasColumnType("text");

                entity.Property(e => e.LeavingSecond).HasColumnType("text");

                entity.Property(e => e.LeavingThird).HasColumnType("text");

                entity.Property(e => e.ObtainFirst).HasColumnType("varchar(35)");

                entity.Property(e => e.ObtainSecond).HasColumnType("varchar(35)");

                entity.Property(e => e.ObtainThird).HasColumnType("varchar(35)");

                entity.Property(e => e.OtherAddressOne).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherAddressThree).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherAddressTwo).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherBenefitsFirst).HasColumnType("varchar(55)");

                entity.Property(e => e.OtherBenefitsSecond).HasColumnType("varchar(55)");

                entity.Property(e => e.OtherBenefitsThird).HasColumnType("varchar(55)");

                entity.Property(e => e.OtherCompanyOne).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherCompanyThree).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherCompanyTwo).HasColumnType("varchar(23)");

                entity.Property(e => e.OtherEndMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherEndMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherEndMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherEndYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherEndYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherEndYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherHoursOne).HasColumnType("varchar(3)");

                entity.Property(e => e.OtherHoursThree).HasColumnType("varchar(3)");

                entity.Property(e => e.OtherHoursTwo).HasColumnType("varchar(3)");

                entity.Property(e => e.OtherLeavingOne).HasColumnType("text");

                entity.Property(e => e.OtherLeavingThree).HasColumnType("text");

                entity.Property(e => e.OtherLeavingTwo).HasColumnType("text");

                entity.Property(e => e.OtherPositionOne).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherPositionThree).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherPositionTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherStartMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherStartMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherStartMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherStartYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherStartYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherStartYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.OtherSupervisorOne).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherSupervisorThree).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherSupervisorTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.OtherUnitOne).HasColumnType("varchar(4)");

                entity.Property(e => e.OtherUnitThree).HasColumnType("varchar(4)");

                entity.Property(e => e.OtherUnitTwo).HasColumnType("varchar(4)");

                entity.Property(e => e.OtherWageOne).HasColumnType("varchar(6)");

                entity.Property(e => e.OtherWageThree).HasColumnType("varchar(6)");

                entity.Property(e => e.OtherWageTwo).HasColumnType("varchar(6)");

                entity.Property(e => e.PositionFirst).HasColumnType("text");

                entity.Property(e => e.PositionSecond).HasColumnType("text");

                entity.Property(e => e.PositionThird).HasColumnType("text");

                entity.Property(e => e.RaiseMonthFirst).HasColumnType("varchar(3)");

                entity.Property(e => e.RaiseMonthSecond).HasColumnType("varchar(3)");

                entity.Property(e => e.RaiseMonthThird).HasColumnType("varchar(3)");

                entity.Property(e => e.RaiseYearFirst).HasColumnType("varchar(4)");

                entity.Property(e => e.RaiseYearSecond).HasColumnType("varchar(4)");

                entity.Property(e => e.RaiseYearThird).HasColumnType("varchar(4)");

                entity.Property(e => e.ReferencesFirst).HasColumnType("text");

                entity.Property(e => e.ReferencesSecond).HasColumnType("text");

                entity.Property(e => e.ReferencesThird).HasColumnType("text");

                entity.Property(e => e.ResponsibilityFirst).HasColumnType("text");

                entity.Property(e => e.ResponsibilitySecond).HasColumnType("text");

                entity.Property(e => e.ResponsibilityThird).HasColumnType("text");

                entity.Property(e => e.StartMonthFirst).HasColumnType("varchar(3)");

                entity.Property(e => e.StartMonthSecond).HasColumnType("varchar(3)");

                entity.Property(e => e.StartMonthThird).HasColumnType("varchar(3)");

                entity.Property(e => e.StartYearFirst).HasColumnType("varchar(4)");

                entity.Property(e => e.StartYearSecond).HasColumnType("varchar(4)");

                entity.Property(e => e.StartYearThird).HasColumnType("varchar(4)");

                entity.Property(e => e.SupervisorFirst).HasColumnType("varchar(80)");

                entity.Property(e => e.SupervisorSecond).HasColumnType("varchar(80)");

                entity.Property(e => e.SupervisorThird).HasColumnType("varchar(80)");

                entity.Property(e => e.TelephoneFirst).HasColumnType("varchar(12)");

                entity.Property(e => e.TelephoneSecond).HasColumnType("varchar(12)");

                entity.Property(e => e.TelephoneThird).HasColumnType("varchar(12)");

                entity.Property(e => e.UnemployedEndMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedEndMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedEndMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedEndYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedEndYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedEndYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedFinanceOne).HasColumnType("varchar(33)");

                entity.Property(e => e.UnemployedFinanceThree).HasColumnType("varchar(33)");

                entity.Property(e => e.UnemployedFinanceTwo).HasColumnType("varchar(33)");

                entity.Property(e => e.UnemployedStartMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedStartMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedStartMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedStartYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedStartYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedStartYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.UnemployedTimeOne).HasColumnType("varchar(35)");

                entity.Property(e => e.UnemployedTimeThree).HasColumnType("varchar(35)");

                entity.Property(e => e.UnemployedTimeTwo).HasColumnType("varchar(35)");

                entity.Property(e => e.UnemployedWeeksOne).HasColumnType("varchar(8)");

                entity.Property(e => e.UnemployedWeeksThree).HasColumnType("varchar(8)");

                entity.Property(e => e.UnemployedWeeksTwo).HasColumnType("varchar(8)");

                entity.Property(e => e.WageEndFirst).HasColumnType("varchar(6)");

                entity.Property(e => e.WageEndSecond).HasColumnType("varchar(6)");

                entity.Property(e => e.WageEndThird).HasColumnType("varchar(6)");

                entity.Property(e => e.WageEndUnitFirst).HasColumnType("varchar(5)");

                entity.Property(e => e.WageEndUnitSecond).HasColumnType("varchar(5)");

                entity.Property(e => e.WageEndUnitThird).HasColumnType("varchar(5)");

                entity.Property(e => e.WageStartFirst).HasColumnType("varchar(6)");

                entity.Property(e => e.WageStartSecond).HasColumnType("varchar(6)");

                entity.Property(e => e.WageStartThird).HasColumnType("varchar(6)");

                entity.Property(e => e.WageStartUnitFirst).HasColumnType("varchar(5)");

                entity.Property(e => e.WageStartUnitSecond).HasColumnType("varchar(5)");

                entity.Property(e => e.WageStartUnitThird).HasColumnType("varchar(5)");

                entity.Property(e => e.WorkFirst)
                    .HasColumnType("char(2)")
                    .HasDefaultValueSql("'00'");

                entity.Property(e => e.WorkHoursFirst).HasColumnType("varchar(3)");

                entity.Property(e => e.WorkHoursSecond).HasColumnType("varchar(3)");

                entity.Property(e => e.WorkHoursThird).HasColumnType("varchar(3)");

                entity.Property(e => e.WorkSecond)
                    .HasColumnType("char(2)")
                    .HasDefaultValueSql("'00'");

                entity.Property(e => e.WorkThird)
                    .HasColumnType("char(2)")
                    .HasDefaultValueSql("'00'");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionD)
                    .HasForeignKey<SectionD>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionD_EmploymentApp");
            });

            modelBuilder.Entity<SectionE>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__sectionE__8D6582C62D27B809");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.AccomplishmentsOne).HasColumnType("text");

                entity.Property(e => e.AccomplishmentsThree).HasColumnType("text");

                entity.Property(e => e.AccomplishmentsTwo).HasColumnType("text");

                entity.Property(e => e.ActivitiesOne).HasColumnType("text");

                entity.Property(e => e.ActivitiesThree).HasColumnType("text");

                entity.Property(e => e.ActivitiesTwo).HasColumnType("text");

                entity.Property(e => e.Apprenticeship).HasColumnType("varchar(70)");

                entity.Property(e => e.ApprenticeshipLocation).HasColumnType("varchar(60)");

                entity.Property(e => e.ApprenticeshipTime).HasColumnType("varchar(20)");

                entity.Property(e => e.AttendedEndMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedEndMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedEndYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedEndYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedStartMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedStartMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedStartYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.AttendedStartYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.Calculator).HasDefaultValueSql("'-1'");

                entity.Property(e => e.CompleteMonthOne).HasColumnType("varchar(2)");

                entity.Property(e => e.CompleteMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.CompleteOne).HasDefaultValueSql("'-1'");

                entity.Property(e => e.CompleteTwo).HasDefaultValueSql("'-1'");

                entity.Property(e => e.CompleteYearOne).HasColumnType("varchar(2)");

                entity.Property(e => e.CompleteYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.CoursesOne).HasColumnType("text");

                entity.Property(e => e.CoursesTwo).HasColumnType("text");

                entity.Property(e => e.DegreeThree).HasColumnType("varchar(20)");

                entity.Property(e => e.DegreeTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.Dictation).HasDefaultValueSql("'-1'");

                entity.Property(e => e.EndMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.EndMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.EndYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.EndYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.EngineerTraining).HasDefaultValueSql("'-1'");

                entity.Property(e => e.EngineerTrainingDate).HasColumnType("varchar(15)");

                entity.Property(e => e.EngineerTrainingState).HasColumnType("varchar(25)");

                entity.Property(e => e.FamilyThree).HasColumnType("varchar(3)");

                entity.Property(e => e.FamilyTwo).HasColumnType("varchar(3)");

                entity.Property(e => e.GpaOne).HasColumnType("varchar(10)");

                entity.Property(e => e.GpaThree).HasColumnType("varchar(10)");

                entity.Property(e => e.GpaTwo).HasColumnType("varchar(10)");

                entity.Property(e => e.GradeOne).HasColumnType("varchar(4)");

                entity.Property(e => e.GradeThree).HasColumnType("varchar(4)");

                entity.Property(e => e.GradeTwo).HasColumnType("varchar(4)");

                entity.Property(e => e.GraduateGed).HasDefaultValueSql("'-1'");

                entity.Property(e => e.GraduateMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.GraduateMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.GraduateOne).HasDefaultValueSql("'-1'");

                entity.Property(e => e.GraduateThree).HasDefaultValueSql("'-1'");

                entity.Property(e => e.GraduateTwo).HasDefaultValueSql("'-1'");

                entity.Property(e => e.GraduateYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.GraduateYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.HighSchoolHours).HasColumnType("text");

                entity.Property(e => e.HonorsOne).HasColumnType("text");

                entity.Property(e => e.HonorsThree).HasColumnType("text");

                entity.Property(e => e.HonorsTwo).HasColumnType("text");

                entity.Property(e => e.LanguageRead).HasColumnType("varchar(20)");

                entity.Property(e => e.LanguageSpeak).HasColumnType("varchar(20)");

                entity.Property(e => e.LanguageWrite).HasColumnType("varchar(20)");

                entity.Property(e => e.LeftPriorOne).HasColumnType("text");

                entity.Property(e => e.LeftPriorThree).HasColumnType("text");

                entity.Property(e => e.LeftPriorTwo).HasColumnType("text");

                entity.Property(e => e.LoanThree).HasColumnType("varchar(3)");

                entity.Property(e => e.LoanTwo).HasColumnType("varchar(3)");

                entity.Property(e => e.MachineSkills).HasColumnType("varchar(75)");

                entity.Property(e => e.MajorOne).HasColumnType("varchar(20)");

                entity.Property(e => e.MajorThree).HasColumnType("varchar(20)");

                entity.Property(e => e.MajorTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.Memberships).HasColumnType("text");

                entity.Property(e => e.MinorOne).HasColumnType("varchar(20)");

                entity.Property(e => e.MinorThree).HasColumnType("varchar(20)");

                entity.Property(e => e.MinorTwo).HasColumnType("varchar(20)");

                entity.Property(e => e.NameOne).HasColumnType("varchar(25)");

                entity.Property(e => e.NameTwo).HasColumnType("varchar(25)");

                entity.Property(e => e.OtherLicenses).HasColumnType("text");

                entity.Property(e => e.OtherTraining).HasColumnType("text");

                entity.Property(e => e.ProfessionalEngineer).HasDefaultValueSql("'-1'");

                entity.Property(e => e.ProfessionalLicense).HasColumnType("varchar(12)");

                entity.Property(e => e.ProfessionalState).HasColumnType("varchar(25)");

                entity.Property(e => e.QuarterHoursThree).HasColumnType("varchar(15)");

                entity.Property(e => e.QuarterHoursTwo).HasColumnType("varchar(15)");

                entity.Property(e => e.ReferenceEmailOne).HasColumnType("varchar(70)");

                entity.Property(e => e.ReferenceEmailThree).HasColumnType("varchar(70)");

                entity.Property(e => e.ReferenceEmailTwo).HasColumnType("varchar(70)");

                entity.Property(e => e.ReferenceNameOne).HasColumnType("varchar(23)");

                entity.Property(e => e.ReferenceNameThree).HasColumnType("varchar(23)");

                entity.Property(e => e.ReferenceNameTwo).HasColumnType("varchar(23)");

                entity.Property(e => e.ReferencePositionOne).HasColumnType("varchar(12)");

                entity.Property(e => e.ReferencePositionThree).HasColumnType("varchar(12)");

                entity.Property(e => e.ReferencePositionTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.ReferenceSchoolOne).HasColumnType("varchar(15)");

                entity.Property(e => e.ReferenceSchoolThree).HasColumnType("varchar(15)");

                entity.Property(e => e.ReferenceSchoolTwo).HasColumnType("varchar(15)");

                entity.Property(e => e.ReferenceTelephoneOne).HasColumnType("varchar(12)");

                entity.Property(e => e.ReferenceTelephoneThree).HasColumnType("varchar(12)");

                entity.Property(e => e.ReferenceTelephoneTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.ScholarshipThree).HasColumnType("varchar(3)");

                entity.Property(e => e.ScholarshipTwo).HasColumnType("varchar(3)");

                entity.Property(e => e.SchoolCityOne).HasColumnType("varchar(10)");

                entity.Property(e => e.SchoolCityThree).HasColumnType("varchar(10)");

                entity.Property(e => e.SchoolCityTwo).HasColumnType("varchar(10)");

                entity.Property(e => e.SchoolLocationOne).HasColumnType("varchar(25)");

                entity.Property(e => e.SchoolLocationTwo).HasColumnType("varchar(25)");

                entity.Property(e => e.SchoolNameOne).HasColumnType("varchar(27)");

                entity.Property(e => e.SchoolNameThree).HasColumnType("varchar(27)");

                entity.Property(e => e.SchoolNameTwo).HasColumnType("varchar(27)");

                entity.Property(e => e.SchoolStateOne).HasColumnType("varchar(2)");

                entity.Property(e => e.SchoolStateThree).HasColumnType("varchar(2)");

                entity.Property(e => e.SchoolStateTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.SemesterHoursThree).HasColumnType("varchar(15)");

                entity.Property(e => e.SemesterHoursTwo).HasColumnType("varchar(15)");

                entity.Property(e => e.Speed).HasColumnType("varchar(3)");

                entity.Property(e => e.Spreadsheet).HasDefaultValueSql("'-1'");

                entity.Property(e => e.SpreadsheetName).HasColumnType("varchar(50)");

                entity.Property(e => e.StartMonthThree).HasColumnType("varchar(2)");

                entity.Property(e => e.StartMonthTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.StartYearThree).HasColumnType("varchar(2)");

                entity.Property(e => e.StartYearTwo).HasColumnType("varchar(2)");

                entity.Property(e => e.TranscriptOne).HasDefaultValueSql("'-1'");

                entity.Property(e => e.TranscriptThree).HasDefaultValueSql("'-1'");

                entity.Property(e => e.TranscriptTwo).HasDefaultValueSql("'-1'");

                entity.Property(e => e.TrimesterHoursThree).HasColumnType("varchar(15)");

                entity.Property(e => e.TrimesterHoursTwo).HasColumnType("varchar(15)");

                entity.Property(e => e.TypeOne).HasColumnType("text");

                entity.Property(e => e.TypeTwo).HasColumnType("text");

                entity.Property(e => e.Typing).HasDefaultValueSql("'-1'");

                entity.Property(e => e.WordProcessing).HasDefaultValueSql("'-1'");

                entity.Property(e => e.WordProcessingName).HasColumnType("varchar(55)");

                entity.Property(e => e.WorkThree).HasColumnType("varchar(3)");

                entity.Property(e => e.WorkTwo).HasColumnType("varchar(3)");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionE)
                    .HasForeignKey<SectionE>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionE_EmploymentApp");
            });

            modelBuilder.Entity<SectionF>(entity =>
            {
                entity.HasKey(e => e.EmploymentAppId)
                    .HasName("PK__section___8D6582C6403A8C7D");

                entity.Property(e => e.EmploymentAppId).ValueGeneratedNever();

                entity.Property(e => e.Branch).HasColumnType("varchar(13)");

                entity.Property(e => e.EndRank).HasColumnType("varchar(13)");

                entity.Property(e => e.Military).HasDefaultValueSql("'-1'");

                entity.Property(e => e.MilitaryEducation).HasColumnType("varchar(70)");

                entity.Property(e => e.MilitaryEmailOne).HasColumnType("varchar(70)");

                entity.Property(e => e.MilitaryEmailThree).HasColumnType("varchar(70)");

                entity.Property(e => e.MilitaryEmailTwo).HasColumnType("varchar(70)");

                entity.Property(e => e.MilitaryEndMonth).HasColumnType("varchar(2)");

                entity.Property(e => e.MilitaryEndYear).HasColumnType("varchar(2)");

                entity.Property(e => e.MilitaryHonors).HasColumnType("text");

                entity.Property(e => e.MilitaryNameOne).HasColumnType("varchar(22)");

                entity.Property(e => e.MilitaryNameThree).HasColumnType("varchar(22)");

                entity.Property(e => e.MilitaryNameTwo).HasColumnType("varchar(22)");

                entity.Property(e => e.MilitaryPositionOne).HasColumnType("varchar(12)");

                entity.Property(e => e.MilitaryPositionThree).HasColumnType("varchar(12)");

                entity.Property(e => e.MilitaryPositionTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.MilitarySchoolOne).HasColumnType("varchar(15)");

                entity.Property(e => e.MilitarySchoolThree).HasColumnType("varchar(15)");

                entity.Property(e => e.MilitarySchoolTwo).HasColumnType("varchar(15)");

                entity.Property(e => e.MilitaryStartMonth).HasColumnType("varchar(2)");

                entity.Property(e => e.MilitaryStartYear).HasColumnType("varchar(2)");

                entity.Property(e => e.MilitaryTelephoneOne).HasColumnType("varchar(12)");

                entity.Property(e => e.MilitaryTelephoneThree).HasColumnType("varchar(12)");

                entity.Property(e => e.MilitaryTelephoneTwo).HasColumnType("varchar(12)");

                entity.Property(e => e.PrincipleDuties).HasColumnType("text");

                entity.Property(e => e.Service).HasColumnType("varchar(13)");

                entity.Property(e => e.StartRank).HasColumnType("varchar(13)");

                entity.HasOne(d => d.EmploymentApp)
                    .WithOne(p => p.SectionF)
                    .HasForeignKey<SectionF>(d => d.EmploymentAppId)
                    .HasConstraintName("FK_SectionF_EmploymentApp");
            });

            modelBuilder.Entity<Slide>(entity =>
            {
                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<SlideItem>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.Slide)
                    .WithMany(p => p.SlideItem)
                    .HasForeignKey(d => d.SlideId)
                    .HasConstraintName("FK_SlideItem_Slide");
            });
        }
    }
}