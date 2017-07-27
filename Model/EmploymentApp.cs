using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class EmploymentApp
    {
        public int EmploymentAppId { get; set; }
        public int? Created { get; set; }
        public int? LastUpdate { get; set; }
        public int? Finished { get; set; }
        public string Status { get; set; }
        public int? Beta { get; set; }
        public string Creator { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? EmailSent { get; set; }

        public virtual SectionA SectionA { get; set; }
        public virtual SectionB SectionB { get; set; }
        public virtual SectionC SectionC { get; set; }
        public virtual SectionD SectionD { get; set; }
        public virtual SectionE SectionE { get; set; }
        public virtual SectionF SectionF { get; set; }
    }
}
